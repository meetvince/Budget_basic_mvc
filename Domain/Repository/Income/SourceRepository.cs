using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Entitites;
using Domain.Repository.Expenses;
using DomainModel.CommonModel;
using DomainModel.Income;
using log4net;
using log4net.Config;

namespace Domain.Repository.Income
{
    public class SourceRepository : ISourceRepository
    {
        private readonly ILog _logger;

        public SourceRepository()
        {
            _logger = LogManager.GetLogger(typeof(ExpensesRepository));
            XmlConfigurator.Configure();
        }

        public IList<PaymentSource> GetAllPaymentSources()
        {
            using (var dbContext = new MyBudget())
            {
                var result = dbContext.paymentSources.ToList();

                Mapper.CreateMap<paymentSource, PaymentSource>()
                    .ForMember(dest=>dest.Id,opts=>opts.MapFrom(src=>src.id))
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.name))
                    .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.description))
                    .ForMember(dest => dest.UpdatedDate, opts => opts.MapFrom(src => src.updatedDate));

                return result.Select(Mapper.Map<paymentSource, PaymentSource>).ToList();
            }
        }

        public bool AddNewPaymentSource(PaymentSource source)
        {
            try
            {
                using (var dbContext = new MyBudget())
                {
                    Mapper.CreateMap<PaymentSource, paymentSource>()
                        .ForMember(dest => dest.name, opts => opts.MapFrom(src => src.Name))
                        .ForMember(dest => dest.description, opts => opts.MapFrom(src => src.Description));

                    var paymentSource = Mapper.Map<PaymentSource, paymentSource>(source);
                    paymentSource.updatedDate = DateTime.Now;

                    dbContext.paymentSources.Add(paymentSource);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
            
        }
    }
}
