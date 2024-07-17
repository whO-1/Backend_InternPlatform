namespace internPlatform.Domain.Entities.DTO
{
    internal interface BaseDTO<T, T_DTO> where T : class
    {
        T_DTO EntityToDTO(T entity);
        T DTOToEntity(T_DTO obj);
    }
}
