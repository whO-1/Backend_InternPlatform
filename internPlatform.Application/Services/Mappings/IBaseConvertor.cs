namespace internPlatform.Application.Services.Mappings
{
    public interface IBaseConvertor<T, T_DTO> where T : class
    {
        T_DTO EntityToDTO(T entity);
        T DTOToEntity(T_DTO obj);
    }
}
