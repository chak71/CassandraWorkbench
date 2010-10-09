/**
 * Autogenerated by Thrift
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Transport;
namespace Apache.Cassandra
{

  [Serializable]
  public partial class IndexExpression : TBase
  {
    private byte[] column_name;
    private IndexOperator op;
    private byte[] value;

    public byte[] Column_name
    {
      get
      {
        return column_name;
      }
      set
      {
        __isset.column_name = true;
        this.column_name = value;
      }
    }

    public IndexOperator Op
    {
      get
      {
        return op;
      }
      set
      {
        __isset.op = true;
        this.op = value;
      }
    }

    public byte[] Value
    {
      get
      {
        return value;
      }
      set
      {
        __isset.value = true;
        this.value = value;
      }
    }


    public Isset __isset;
    [Serializable]
    public struct Isset {
      public bool column_name;
      public bool op;
      public bool value;
    }

    public IndexExpression() {
    }

    public void Read (TProtocol iprot)
    {
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.String) {
              this.column_name = iprot.ReadBinary();
              this.__isset.column_name = true;
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.I32) {
              this.op = (IndexOperator)iprot.ReadI32();
              this.__isset.op = true;
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.String) {
              this.value = iprot.ReadBinary();
              this.__isset.value = true;
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          default: 
            TProtocolUtil.Skip(iprot, field.Type);
            break;
        }
        iprot.ReadFieldEnd();
      }
      iprot.ReadStructEnd();
    }

    public void Write(TProtocol oprot) {
      TStruct struc = new TStruct("IndexExpression");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (this.column_name != null && __isset.column_name) {
        field.Name = "column_name";
        field.Type = TType.String;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteBinary(this.column_name);
        oprot.WriteFieldEnd();
      }
      if (__isset.op) {
        field.Name = "op";
        field.Type = TType.I32;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int)this.op);
        oprot.WriteFieldEnd();
      }
      if (this.value != null && __isset.value) {
        field.Name = "value";
        field.Type = TType.String;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteBinary(this.value);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("IndexExpression(");
      sb.Append("column_name: ");
      sb.Append(this.column_name);
      sb.Append(",op: ");
      sb.Append(this.op);
      sb.Append(",value: ");
      sb.Append(this.value);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
