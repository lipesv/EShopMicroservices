﻿namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        // TODO: Get orders with pagination

        var pageIndex = query.PaginatonRequest.PageIndex;
        var pageSize = query.PaginatonRequest.PageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        // return result

        var orders = await dbContext.Orders.Include(o => o.OrderItems)
                                           .AsNoTracking()
                                           .OrderBy(o => o.OrderName.Value)
                                           .Skip(pageIndex * pageSize)
                                           .Take(pageSize)
                                           .ToListAsync(cancellationToken);

        return new GetOrdersResult(new PaginatedResult<OrderDto>(pageIndex,
                                                                 pageSize,
                                                                 totalCount,
                                                                 orders.ToOrderDtoList()));
    }
}
