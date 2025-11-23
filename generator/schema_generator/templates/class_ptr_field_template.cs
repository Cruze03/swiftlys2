  private static readonly nint _$NAME$Offset = Schema.GetOffset($HASH$);

  public $INTERFACE_TYPE$? $NAME$ {
    get {
      var ptr = _Handle.Read<nint>(_$NAME$Offset);
      return ptr.IsValidPtr() ? new $IMPL_TYPE$(ptr) : null;
    }
  }