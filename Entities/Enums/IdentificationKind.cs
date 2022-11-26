namespace Entities.Enums
{
    public enum IdentificationKind
    {
        [EnumAlias("0 - При личном присутствии")] Personal,
        [EnumAlias("1 - Без личного присутствия с использованием квалифицированной ЭП")] RemoteCert,
        [EnumAlias("2 - Без личного присутствия с использованием персональных данных, записанных на электронный носитель из заграничного паспорта")] RemotePassport,
        [EnumAlias("3 - Без личного присутствия с использованием сведений из ЕСИА и ЕБС")] RemoteSystem, 
    }
}