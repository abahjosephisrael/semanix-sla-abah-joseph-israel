using FormsService.Domain.Enums;

namespace FormsService.Domain.Dtos;

public record FieldDto(string FieldId, string Label, FieldType Type, int Order, bool Required = false, int? MaxLength = null);
