  private static readonly nint _$NAME$Offset = Schema.GetOffset($HASH$);

  public string $NAME$ {
    get {
      var ptr = _Handle.Read<nint>(_$NAME$Offset);
      return Schema.GetString(ptr);
    }
    set => Schema.SetString(_Handle, _$NAME$Offset, value);
  } 