using AutoMapper;
using BB.DAL.Context;

namespace BB.BLL.Services.Abstract
{
    public abstract class BaseService
    {

        protected readonly BBContext Context;
        protected readonly IMapper Mapper;

        protected BaseService(BBContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
    }
}