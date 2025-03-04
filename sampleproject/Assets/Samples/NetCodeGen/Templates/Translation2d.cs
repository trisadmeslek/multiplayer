#region __GHOST_IMPORTS__
#endregion
namespace Generated
{
    public struct GhostSnapshotData
    {
        public struct Snapshot
        {
            #region __GHOST_FIELD__
            public int __GHOST_FIELD_NAME__X;
            public int __GHOST_FIELD_NAME__Y;
            #endregion
        }

        public void PredictDelta(uint tick, ref GhostSnapshotData baseline1, ref GhostSnapshotData baseline2)
        {
            var predictor = new GhostDeltaPredictor(tick, this.tick, baseline1.tick, baseline2.tick);
            #region __GHOST_PREDICT__
            snapshot.__GHOST_FIELD_NAME__X = predictor.PredictInt(snapshot.__GHOST_FIELD_NAME__X, baseline1.__GHOST_FIELD_NAME__X, baseline2.__GHOST_FIELD_NAME__X);
            snapshot.__GHOST_FIELD_NAME__Y = predictor.PredictInt(snapshot.__GHOST_FIELD_NAME__Y, baseline1.__GHOST_FIELD_NAME__Y, baseline2.__GHOST_FIELD_NAME__Y);
            #endregion
        }

        public void Serialize(ref Snapshot snapshot, ref Snapshot baseline, ref DataStreamWriter writer, ref NetworkCompressionModel compressionModel, uint changeMask)
        {
            #region __GHOST_WRITE__
            if ((changeMask__GHOST_MASK_BATCH__ & (1 << __GHOST_MASK_INDEX__)) != 0)
            {
                writer.WritePackedIntDelta(snapshot.__GHOST_FIELD_NAME__X, baseline.__GHOST_FIELD_NAME__X, compressionModel);
                writer.WritePackedIntDelta(snapshot.__GHOST_FIELD_NAME__Y, baseline.__GHOST_FIELD_NAME__Y, compressionModel);
            }
            #endregion
        }

        public void Deserialize(ref Snapshot snapshot, ref Snapshot baseline, ref DataStreamReader reader, ref NetworkCompressionModel compressionModel, uint changeMask)
        {
            #region __GHOST_READ__
            if ((changeMask__GHOST_MASK_BATCH__ & (1 << __GHOST_MASK_INDEX__)) != 0)
            {
                snapshot.__GHOST_FIELD_NAME__X = reader.ReadPackedIntDelta(baseline.__GHOST_FIELD_NAME__X, compressionModel);
                snapshot.__GHOST_FIELD_NAME__Y = reader.ReadPackedIntDelta(baseline.__GHOST_FIELD_NAME__Y, compressionModel);
            }
            else
            {
                snapshot.__GHOST_FIELD_NAME__X = baseline.__GHOST_FIELD_NAME__X;
                snapshot.__GHOST_FIELD_NAME__Y = baseline.__GHOST_FIELD_NAME__Y;
            }
            #endregion
        }
        public unsafe void CopyToSnapshot(ref Snapshot snapshot, ref Translation component)
        {
            if (true)
            {
                #region __GHOST_COPY_TO_SNAPSHOT__
                snapshot.__GHOST_FIELD_NAME__X = (int)(component.__GHOST_FIELD_REFERENCE__.x * __GHOST_QUANTIZE_SCALE__);
                snapshot.__GHOST_FIELD_NAME__Y = (int)(component.__GHOST_FIELD_REFERENCE__.y * __GHOST_QUANTIZE_SCALE__);
                #endregion
            }
        }
        public unsafe void CopyFromSnapshot(ref GhostDeserializerState deserializerState, ref Snapshot snapshotBefore, ref Snapshot snapshotAfter, float snapshotInterpolationFactor, ref Translation component)
        {
            if (true)
            {
                #region __GHOST_COPY_FROM_SNAPSHOT__
                component.Value = new float3(snapshotBefore.__GHOST_FIELD_NAME__X * __GHOST_DEQUANTIZE_SCALE__, snapshotBefore.__GHOST_FIELD_NAME__Y * __GHOST_DEQUANTIZE_SCALE__, 0.0f));
                #endregion

                #region __GHOST_COPY_FROM_SNAPSHOT_INTERPOLATE__
                component.__GHOST_FIELD_REFERENCE__ = math.lerp(
                    new float3(snapshotBefore.__GHOST_FIELD_NAME__X * __GHOST_DEQUANTIZE_SCALE__, snapshotBefore.__GHOST_FIELD_NAME__Y * __GHOST_DEQUANTIZE_SCALE__, 0.0f),
                    new float3(snapshotAfter.__GHOST_FIELD_NAME__X * __GHOST_DEQUANTIZE_SCALE__, snapshotAfter.__GHOST_FIELD_NAME__Y * __GHOST_DEQUANTIZE_SCALE__, 0.0f),
                    snapshotInterpolationFactor);
                #endregion
            }
        }
        public unsafe void RestoreFromBackup(ref IComponentData component, in IComponentData backup)
        {
            #region __GHOST_RESTORE_FROM_BACKUP__
            component.__GHOST_FIELD_REFERENCE__ = backup.__GHOST_FIELD_REFERENCE__;
            #endregion
        }
        public void CalculateChangeMask(ref Snapshot snapshot, ref Snapshot baseline, uint changeMask)
        {
            #region __GHOST_CALCULATE_CHANGE_MASK_ZERO__
            changeMask = (snapshot.__GHOST_FIELD_NAME__X != baseline.__GHOST_FIELD_NAME__X ||
                        snapshot.__GHOST_FIELD_NAME__Y != baseline.__GHOST_FIELD_NAME__Y)
                        ? 1u : 0;
            #endregion
            #region __GHOST_CALCULATE_CHANGE_MASK__
            changeMask |= (snapshot.__GHOST_FIELD_NAME__X != baseline.__GHOST_FIELD_NAME__X ||
                        snapshot.__GHOST_FIELD_NAME__Y != baseline.__GHOST_FIELD_NAME__Y)
                        ? (1u<<__GHOST_MASK_INDEX__) : 0;
            #endregion
        }
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        private static void ReportPredictionErrors(ref IComponentData component, in IComponentData backup, ref UnsafeList<float> errors, ref int errorIndex)
        {
            #region __GHOST_REPORT_PREDICTION_ERROR__
            errors[errorIndex] = math.max(errors[errorIndex], math.distance(component.__GHOST_FIELD_REFERENCE__, backup.__GHOST_FIELD_REFERENCE__));
            ++errorIndex;
            #endregion
        }
        private static int GetPredictionErrorNames(ref FixedString512Bytes names, ref int nameCount)
        {
            #region __GHOST_GET_PREDICTION_ERROR_NAME__
            if (nameCount != 0)
                names.Append(new FixedString32Bytes(","));
            names.Append(new FixedString64Bytes("__GHOST_FIELD_REFERENCE__"));
            ++nameCount;
            #endregion
        }
        #endif
    }
}