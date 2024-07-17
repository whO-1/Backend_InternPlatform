namespace internPlatform.Application.Services.Mappings
{
    public interface IBaseConvertor<T, T_DTO>
        where T : class
        where T_DTO : class
    {
        T_DTO EntityToDTO(T entity);
        T DTOToEntity(T_DTO obj);
    }
}
