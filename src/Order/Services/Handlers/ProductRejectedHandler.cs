﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderApi.Data.Context;
using OrderApi.Data.Entities;
using OrderApi.Domain.Events.Product;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApi.Services.Handlers
{
    public class ProductRejectedHandler : IRequestHandler<ProductRejected>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductRejectedHandler> _logger;

        public ProductRejectedHandler(
            IServiceScopeFactory serviceScopeFactory,
            IMapper mapper,
            ILogger<ProductRejectedHandler> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(ProductRejected productRejected, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"---- Received {nameof(ProductRejected)} message: Product.Guid = [{productRejected.Guid}] ----");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrderContext>();
                var product = await context.Products.FirstOrDefaultAsync(product => product.Guid.Equals(productRejected.Guid));

                if (product != null)
                {
                    _logger.LogWarning($"---- Product.Guid = [{productRejected.Guid}] exists and will be removed ----");
                    context.Products.Remove(product);
                    await context.SaveChangesAsync();
                }
            }
            _logger.LogInformation($"---- Removed {nameof(Product)} with Guid = [{productRejected.Guid}] ----");

            return await Task.FromResult(Unit.Value);
        }
    }
}