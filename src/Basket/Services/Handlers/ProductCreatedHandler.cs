﻿using BasketApi.Domain.Events.Product;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BasketApi.Services.Handlers
{
    public class ProductCreatedHandler : IRequestHandler<ProductCreated>
    {
        private readonly ILogger<ProductCreatedHandler> _logger;

        public ProductCreatedHandler(ILogger<ProductCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task<Unit> Handle(ProductCreated request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"---- Received {nameof(ProductCreated)} message: {request.Name} ----");
            return Task.FromResult(Unit.Value);
        }
    }
}