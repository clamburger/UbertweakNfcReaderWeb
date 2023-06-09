﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PCSC.Monitoring;
using UbertweakNfcReaderWeb.Messaging;
using UbertweakNfcReaderWeb.Models;

namespace UbertweakNfcReaderWeb.Services
{
    public class PlexusService
    {
        private readonly NfcService _nfc;
        private readonly IMediator _mediator;

        public string? PrimaryConnection;

        public PlexusService(NfcService nfc, IMediator mediator)
        {
            _nfc = nfc;
            _mediator = mediator;
        }

        public void Watch()
        {
            _nfc.CardInserted += CardInserted;
            _nfc.CardRemoved += CardRemoved;
        }

        public void StopWatch()
        {
            _nfc.CardInserted -= CardInserted;
            _nfc.CardRemoved -= CardRemoved;
        }

        private void CardInserted(object? sender, CardInsertedEventArgs e)
        {
            using var db = new DatabaseContext();
            var card = db.Cards
                .Include(c => c.User)
                .ThenInclude(u => u.Team)
                .FirstOrDefault(c => c.Uid == e.Uid) ?? new AnyCard { Uid = e.Uid };

            _mediator.Publish(new CardInserted { Card = card });
        }

        private void CardRemoved(object? sender, CardStatusEventArgs e)
        {
            _mediator.Publish(new CardRemoved());
        }
    }
}
