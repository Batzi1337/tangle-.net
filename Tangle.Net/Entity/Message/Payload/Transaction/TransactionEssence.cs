﻿namespace Tangle.Net.Entity.Message.Payload.Transaction
{
  using System;
  using System.Collections.Generic;

  using Newtonsoft.Json;

  public class TransactionEssence : PayloadType, ISerializable
  {
    [JsonProperty("inputs")]
    public List<UTXOInput> Inputs { get; set; }

    [JsonProperty("outputs")]
    public List<SigLockedSingleOutput> Outputs { get; set; }

    [JsonProperty("payload")]
    public IndexationPayload Payload { get; set; }

    public byte[] Serialize()
    {
      var serialized = new List<byte>();
      serialized.Add((byte)this.Type);
      serialized.AddRange(this.SerializeInputs());
      serialized.AddRange(this.SerializeOutputs());
      serialized.AddRange(this.Payload.Serialize());

      return serialized.ToArray();
    }

    public byte[] SerializeOutputs()
    {
      var serializedOutputs = new List<byte>();
      serializedOutputs.AddRange(BitConverter.GetBytes((short)this.Outputs.Count));
      foreach (var output in this.Outputs)
      {
        serializedOutputs.AddRange(output.Serialize());
      }

      return serializedOutputs.ToArray();
    }

    public byte[] SerializeInputs()
    {
      var serializedInputs = new List<byte>();
      serializedInputs.AddRange(BitConverter.GetBytes((short)this.Inputs.Count));
      foreach (var input in this.Inputs)
      {
        serializedInputs.AddRange(input.Serialize());
      }

      return serializedInputs.ToArray();
    }
  }
}