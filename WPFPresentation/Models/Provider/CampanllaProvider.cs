
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using AutoMapper;
using Data_Layer.Implementations;
using Domain_Layer.Entities;


namespace WPFPresentation.Models.Provider
{
    public class CampanllaProvider : BaseProvider<CampanllaModel, Campanlla>
    {
        public CampanllaProvider(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void CreateBaseRepository()
        {
            _baseRepository = UnitOfWork.CampanllaRepository;
        }
    }
}
