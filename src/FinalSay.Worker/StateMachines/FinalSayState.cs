using System;
using System.Collections.Generic;
using FinalSay.Contracts;
using MassTransit;

namespace FinalSay.Worker.StateMachines;

public class FinalSayState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }

    public int CurrentState { get; set; }

    public List<Decisions> Decisions { get; set; } = [];

    public DateTime SubmittedAt { get; set; }

    public DateTime? DecidedAt { get; set; }
    public Guid? RequestId { get; set; }
    public Uri? ResponseAddress { get; set; }
}