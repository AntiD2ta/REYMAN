using BizDbAccess.GenericInterfaces;
using BizLogic.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.BizRunners
{
    public class RunnerWriteDb<TIn, TOut>
    {
        private readonly IBizAction<TIn, TOut> _actionClass;
        private readonly IUnitOfWork _context;

        public IImmutableList<ValidationResult>
            Errors => _actionClass.Errors;

        public bool HasErrors => _actionClass.HasErrors;

        public RunnerWriteDb(IBizAction<TIn, TOut> actionClass,
            IUnitOfWork context)
        {
            _context = context;
            _actionClass = actionClass;
        }

        public TOut RunAction(TIn dataIn)
        {
            var result = _actionClass.Action(dataIn);
            if (!HasErrors)
                _context.Commit();
            return result;
        }
    }
}
