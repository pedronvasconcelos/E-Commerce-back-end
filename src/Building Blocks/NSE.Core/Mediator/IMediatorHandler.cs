﻿using System.Threading.Tasks;
using FluentValidation.Results;
using NSE.Core.Messages;

namespace NSE.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<ValidationResult> SendCommand<T>(T comando) where T : Command;
    }
}