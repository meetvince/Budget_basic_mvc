using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Entitites;
using DomainModel;
using DomainModel.CommonModel;
using DomainModel.Expenses;
using log4net;
using log4net.Config;

namespace Domain.Repository
{
    public class ExpensesRepository : IExpensesRepository
    {
        private readonly ILog _logger;

        public ExpensesRepository()
        {
            _logger = LogManager.GetLogger(typeof(ExpensesRepository));
            XmlConfigurator.Configure();
        }

        public IList<PaymentDestination> GetAllExpenseDestinations()
        {
            using (var context = new MyBudget())
            {
                var result = context.expenseDestinations.ToList();

                Mapper.CreateMap<expenseDestination, PaymentDestination>()
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.name_))
                    .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.description))
                    .ForMember(dest => dest.UpdatedDate, opts => opts.MapFrom(src => src.updatedDate))
                    .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.id));

                return result.Select(Mapper.Map<expenseDestination, PaymentDestination>).ToList();
            }
        }

        public bool AddNewExpenseDestination(PaymentDestination expenseDestination)
        {
            try
            {
                using (var context = new MyBudget())
                {
                    Mapper.CreateMap<PaymentDestination, expenseDestination>();
                    var dest = Mapper.Map<PaymentDestination, expenseDestination>(expenseDestination);
                    dest.updatedDate = DateTime.Now;
                    context.expenseDestinations.Add(dest);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message, ex);
                Console.WriteLine(ex.Message);
                return false;
            }
            _logger.Debug("Successfully inserted new payment destination");
            return true;
        }

        //public bool UpdateExpenseDestination(PaymentDestination expenseDestination)
        //{
        //    try
        //    {
        //        using (var context = new MyBudget())
        //        {
        //            Mapper.CreateMap<PaymentDestination, expenseDestination>();
        //            var dest = Mapper.Map<PaymentDestination, expenseDestination>(expenseDestination);
        //            dest.updatedDate = DateTime.Now;
        //            context.Entry(expenseDestination).
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Debug(ex.Message, ex);
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //    _logger.Debug("Successfully inserted new payment destination");
        //    return true;
        //}

        public PaymentDestination FindPaymentDestination(int paymentDestinationId)
        {
            try
            {
                using (var context = new MyBudget())
                {
                    var result = context.expenseDestinations.Where(x => x.id == paymentDestinationId);
                    if (result.Any())
                    {
                        Mapper.CreateMap<expenseDestination, PaymentDestination>()
                           .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.name_))
                           .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.description))
                           .ForMember(dest => dest.UpdatedDate, opts => opts.MapFrom(src => src.updatedDate));

                        _logger.Debug("Successfully retrieved payment destination");
                        return Mapper.Map<expenseDestination, PaymentDestination>(result.First());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message, ex);
                Console.WriteLine(ex.Message);
            }

            return null;

        }
    }
}
