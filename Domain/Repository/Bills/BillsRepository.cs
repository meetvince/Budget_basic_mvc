
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Entitites;
using Domain.Repository.Expenses;
using DomainModel.Bills;
using DomainModel.CommonModel;
using log4net;
using log4net.Config;

namespace Domain.Repository.Bills
{
    public class BillsRepository : IBillsRepository
    {
        private readonly ILog _logger;

        public BillsRepository()
        {
            _logger = LogManager.GetLogger(typeof(ExpensesRepository));
            XmlConfigurator.Configure();
        }

        public bool AddNewBillPayment(BillPayment bill)
        {
            try
            {
                Mapper.CreateMap<BillPayment, bill>()
                       .ForMember(dest => dest.amount, opts => opts.MapFrom(src => src.Amount))
                       .ForMember(dest => dest.expenseDestinationId, opts => opts.MapFrom(src => src.ExpenseDestinationId))
                       .ForMember(dest => dest.transactionDate, opts => opts.MapFrom(src => src.TransactionDateTime));

                using (var dbContext = new MyBudget())
                {
                   
                    var billEntity = Mapper.Map<BillPayment, bill>(bill);

                    dbContext.bills.Add(billEntity);

                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(String.Format("Exception occurred while saving bill entity. Exception message :{0}",ex.Message));
                return false;
            }
        }

        public IList<BillPayment> GetAllBills()
        {
            try
            {
                Mapper.CreateMap<bill, BillPayment>()
                       .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.amount))
                       .ForMember(dest => dest.ExpenseDestinationId, opts => opts.MapFrom(src => src.expenseDestinationId))
                       .ForMember(dest => dest.TransactionDateTime, opts => opts.MapFrom(src => src.transactionDate));

                using (var dbContext = new MyBudget())
                {
                    return dbContext.bills.ToList().Select(Mapper.Map<bill, BillPayment>).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(String.Format("Exception occurred while retrieving bill entity. Exception message :{0}", ex.Message));
                return null;
            }
            
        }
    }
}
