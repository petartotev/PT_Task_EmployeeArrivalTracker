using WebAppServer.Entities;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Tests.Infrastructure.Factories;

public static class Factory
{
    public static class Arrivals
    {
        public static class Contracts
        {
            public static class Request
            {
                public static ArrivalRequestContract Create(Action<ArrivalRequestContract> setup = null)
                {
                    var request = new ArrivalRequestContract
                    {
                        Skip = 0,
                        Take = 55,
                        Order = "DESC",
                        FromDate = DateTime.Now,
                        ToDate = DateTime.Now
                    };

                    setup?.Invoke(request);

                    return request;
                }
            }

            public static class Response
            {
                public static Page<ArrivalResponseContract> Create(Action<Page<ArrivalResponseContract>> setup = null)
                {
                    var response = new Page<ArrivalResponseContract>
                    {
                        TotalItems = 1,
                        TotalPages = 1,
                        CurrentPage = 1,
                        ItemsPerPage = 1,
                        Items = new List<ArrivalResponseContract>()
                    };

                    setup?.Invoke(response);

                    return response;
                }
            }
        }

        public static class Repos
        {
            public static class Response
            {
                public static Dapperer.Page<ArrivalEntity> Create(Action<Dapperer.Page<ArrivalEntity>> setup = null)
                {
                    var page = new Dapperer.Page<ArrivalEntity>
                    {
                        TotalItems = 1,
                        TotalPages = 1,
                        CurrentPage = 1,
                        ItemsPerPage = 55,
                        Items = new()
                    };

                    setup?.Invoke(page);

                    return page;
                }
            }
        }
    }
}
