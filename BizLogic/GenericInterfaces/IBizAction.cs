using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.GenericInterfaces
{
    public interface IBizAction<in TIn, out TOut>
    {
        IImmutableList<ValidationResult> Errors { get; }
        bool HasErrors { get; }
        TOut Action(TIn dto);
    }
}
