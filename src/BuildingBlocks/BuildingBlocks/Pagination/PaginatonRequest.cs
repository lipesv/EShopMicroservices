namespace BuildingBlocks.Pagination;

public record PaginatonRequest(int PageIndex = 0, int PageSize = 10);
