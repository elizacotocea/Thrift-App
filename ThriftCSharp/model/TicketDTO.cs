using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;


namespace model
{
#if !SILVERLIGHT
    [Serializable]
#endif
   public partial class TicketDTO: TBase
    {
        private int _id;
        private int _nrWantedSeats;
        private string _buyerName;
        private int _idShow;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                __isset.id = true;
                this._id = value;
            }
        }

        public int NrWantedSeats
        {
            get
            {
                return _nrWantedSeats;
            }
            set
            {
                __isset.nrWantedSeats = true;
                this._nrWantedSeats = value;
            }
        }

        public string BuyerName
        {
            get
            {
                return _buyerName;
            }
            set
            {
                __isset.buyerName = true;
                this._buyerName = value;
            }
        }

        public int IdShow
        {
            get
            {
                return _idShow;
            }
            set
            {
                __isset.idShow = true;
                this._idShow = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool id;
            public bool nrWantedSeats;
            public bool buyerName;
            public bool idShow;
        }

        public TicketDTO()
        {
        }

        public void Read(TProtocol iprot)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                TField field;
                iprot.ReadStructBegin();
                while (true)
                {
                    field = iprot.ReadFieldBegin();
                    if (field.Type == TType.Stop)
                    {
                        break;
                    }
                    switch (field.ID)
                    {
                        case 1:
                            if (field.Type == TType.I32)
                            {
                                Id = iprot.ReadI32();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 2:
                            if (field.Type == TType.I32)
                            {
                                NrWantedSeats = iprot.ReadI32();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 3:
                            if (field.Type == TType.String)
                            {
                                BuyerName = iprot.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 4:
                            if (field.Type == TType.I32)
                            {
                                IdShow = iprot.ReadI32();
                            }
                            else
                            {
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
            finally
            {
                iprot.DecrementRecursionDepth();
            }
        }

        public void Write(TProtocol oprot)
        {
            oprot.IncrementRecursionDepth();
            try
            {
                TStruct struc = new TStruct("TicketDTO");
                oprot.WriteStructBegin(struc);
                TField field = new TField();
                if (__isset.id)
                {
                    field.Name = "id";
                    field.Type = TType.I32;
                    field.ID = 1;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteI32(Id);
                    oprot.WriteFieldEnd();
                }
                if (__isset.nrWantedSeats)
                {
                    field.Name = "nrWantedSeats";
                    field.Type = TType.I32;
                    field.ID = 2;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteI32(NrWantedSeats);
                    oprot.WriteFieldEnd();
                }
                if (BuyerName != null && __isset.buyerName)
                {
                    field.Name = "buyerName";
                    field.Type = TType.String;
                    field.ID = 3;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(BuyerName);
                    oprot.WriteFieldEnd();
                }
                if (__isset.idShow)
                {
                    field.Name = "idShow";
                    field.Type = TType.I32;
                    field.ID = 4;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteI32(IdShow);
                    oprot.WriteFieldEnd();
                }
                oprot.WriteFieldStop();
                oprot.WriteStructEnd();
            }
            finally
            {
                oprot.DecrementRecursionDepth();
            }
        }

        public override string ToString()
        {
            StringBuilder __sb = new StringBuilder("TicketDTO(");
            bool __first = true;
            if (__isset.id)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Id: ");
                __sb.Append(Id);
            }
            if (__isset.nrWantedSeats)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("NrWantedSeats: ");
                __sb.Append(NrWantedSeats);
            }
            if (BuyerName != null && __isset.buyerName)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("BuyerName: ");
                __sb.Append(BuyerName);
            }
            if (__isset.idShow)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("IdShow: ");
                __sb.Append(IdShow);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }


}

