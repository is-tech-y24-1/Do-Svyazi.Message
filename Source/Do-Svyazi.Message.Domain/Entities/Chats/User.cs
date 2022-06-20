using RichEntity.Annotations;

namespace Do_Svyazi.Message.Domain.Entities;

[ConfigureConstructors(ParametrizedConstructorAccessibility = Accessibility.Public)]
public partial class User : IEntity<Guid> { }