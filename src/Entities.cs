using System;
using System.Runtime.Serialization;

namespace OdataExample {
    [DataContract]
    public abstract class BaseApiModel {
        [DataMember (Name = "id", EmitDefaultValue = false)]
        public virtual long Id { get; set; }

        [DataMember (Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
    }

    [DataContract]
    public class InstrumentType : BaseApiModel {
        [DataMember (Name = "instrumentCode", EmitDefaultValue = false)]
        public string InstrumentCode { get; set; }
    }

    [DataContract]
    public class Instrument : BaseApiModel {
        [DataMember (Name = "instrumentType", EmitDefaultValue = false)]
        public InstrumentType InstrumentType { get; set; }
    }

    [DataContract]
    public class Precious : Instrument { }

    [DataContract]
    public class Bullion : Instrument {
        [DataMember (Name = "baseInstrument", EmitDefaultValue = false)]
        public Instrument BaseInstrument { get; set; }

        [DataMember (Name = "ligatureMass", EmitDefaultValue = false)]
        public decimal? LigatureMass { get; set; }
    }

    [DataContract]
    public class PaymentApiModel {
        [DataMember (Name = "id")]
        public long Id { get; set; }

        [DataMember (Name = "instrument")]
        public Instrument Instrument { get; set; }

        [DataMember (Name = "amount")]
        public decimal Amount { get; set; }

        [DataMember (Name = "date")]
        public DateTimeOffset Date { get; set; }
    }
}