  private static readonly nint _$NAME$Offset = Schema.GetOffset($HASH$);

  public string $NAME$ {
    get {
      var ptr = _Handle + _$NAME$Offset;
      return Schema.GetString(ptr);
    }
    set => Schema.SetFixedString(_Handle, _$NAME$Offset, value, $ELEMENT_COUNT$);
  } 