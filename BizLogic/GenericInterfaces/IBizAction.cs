using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.GenericInterfaces
{
    /// <summary>
    /// Represents a object in charge of add entities to the database,
    /// and to validate its data at the lowest level.
    /// </summary>
    /// <typeparam name="TIn">Data in</typeparam>
    /// <typeparam name="TOut">Data out</typeparam>
    public interface IBizAction<in TIn, out TOut>
    {
        IImmutableList<ValidationResult> Errors { get; }
        bool HasErrors { get; }
        TOut Action(TIn dto);
    }
}
