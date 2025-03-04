#region __GHOST_IMPORTS__
#endregion
namespace Generated
{
    public struct GhostSnapshotData
    {
        public struct Snapshot
        {
            #region __GHOST_FIELD__
            public int __GHOST_FIELD_NAME__W;
            #endregion
        }

        public void PredictDelta(uint tick, ref GhostSnapshotData baseline1, ref GhostSnapshotData baseline2)
        {
            var predictor = new GhostDeltaPredictor(tick, this.tick, baseline1.tick, baseline2.tick);
            #region __GHOST_PREDICT__
            snapshot.__GHOST_FIELD_NAME__W = predictor.PredictInt(snapshot.__GHOST_FIELD_NAME__W, baseline1.__GHOST_FIELD_NAME__W, baseline2.__GHOST_FIELD_NAME__W);
            #endregion
        }

        public void Serialize(ref Snapshot snapshot, ref Snapshot baseline, ref DataStreamWriter writer, ref NetworkCompressionModel compressionModel, uint changeMask)
        {
            #region __GHOST_WRITE__
            if ((changeMask__GHOST_MASK_BATCH__ & (1 << __GHOST_MASK_INDEX__)) != 0)
            {
                writer.WritePackedIntDelta(snapshot.__GHOST_FIELD_NAME__W, baseline.__GHOST_FIELD_NAME__W, compressionModel);
            }
            #endregion
        }

        public void Deserialize(ref Snapshot snapshot, ref Snapshot baseline, ref DataStreamReader reader, ref NetworkCompressionModel compressionModel, uint changeMask)
        {
            #region __GHOST_READ__
            if ((changeMask__GHOST_MASK_BATCH__ & (1 << __GHOST_MASK_INDEX__)) != 0)
            {
                snapshot.__GHOST_FIELD_NAME__W = reader.ReadPackedIntDelta(baseline.__GHOST_FIELD_NAME__W, compressionModel);
            }
            else
            {
                snapshot.__GHOST_FIELD_NAME__W = baseline.__GHOST_FIELD_NAME__W;
            }
            #endregion
        }
        public unsafe void CopyToSnapshot(ref Snapshot snapshot, ref IComponentData component)
        {
            if (true)
            {
                #region __GHOST_COPY_TO_SNAPSHOT__
                snapshot.__GHOST_FIELD_NAME__W = (int)(component.__GHOST_FIELD_REFERENCE__.value.z > 0.0f
                    ? component.__GHOST_FIELD_REFERENCE__.value.w * __GHOST_QUANTIZE_SCALE__
                    : -component.__GHOST_FIELD_REFERENCE__.value.w * __GHOST_QUANTIZE_SCALE__);
                #endregion
            }
        }
        public unsafe void CopyFromSnapshot(ref GhostDeserializerState deserializerState, ref Snapshot snapshotBefore, ref Snapshot snapshotAfter, float snapshotInterpolationFactor, ref IComponentData component)
        {
            if (true)
            {
                #region __GHOST_COPY_FROM_SNAPSHOT__
                var wb = snapshotBefore.__GHOST_FIELD_NAME__W * __GHOST_DEQUANTIZE_SCALE__;
                component.__GHOST_FIELD_REFERENCE__ = new quaternion(new float4(0f,0f, wb>1.0f-1e-9f?0.0f:math.sqrt(1f - wb*wb), wb));
                #endregion

                #region __GHOST_COPY_FROM_SNAPSHOT_INTERPOLATE__
                var wb = snapshotBefore.__GHOST_FIELD_NAME__W * __GHOST_DEQUANTIZE_SCALE__;
                var wa = snapshotAfter.__GHOST_FIELD_NAME__W * __GHOST_DEQUANTIZE_SCALE__;
                component.__GHOST_FIELD_REFERENCE__ = math.slerp(
                    new quaternion(new float4(0f,0f, wb>1.0f-1e-9f?0.0f:math.sqrt(1f - wb*wb), wb)),
                    new quaternion(new float4(0f,0f, wa>1.0f-1e-9f?0.0f:math.sqrt(1f - wa*wa), wa)),
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
            changeMask = (snapshot.__GHOST_FIELD_NAME__W != baseline.__GHOST_FIELD_NAME__W) ? 1u : 0;
            #endregion
            #region __GHOST_CALCULATE_CHANGE_MASK__
            changeMask |= (snapshot.__GHOST_FIELD_NAME__W != baseline.__GHOST_FIELD_NAME__W) ? (1u<<__GHOST_MASK_INDEX__) : 0;
            #endregion
        }
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        private static void ReportPredictionErrors(ref IComponentData component, in IComponentData backup, ref UnsafeList<float> errors, ref int errorIndex)
        {
            #region __GHOST_REPORT_PREDICTION_ERROR__
            errors[errorIndex] = math.max(errors[errorIndex], math.distance(component.__GHOST_FIELD_REFERENCE__.value, backup.__GHOST_FIELD_REFERENCE__.value));
            ++errorIndex;
            #endregion
        }
        private static int GetPredictionErrorNames(ref FixedString512Bytes names, ref int nameCount)
        {
            #region __GHOST_GET_PREDICTION_ERROR_NAME__
            if (nameCount != 0)
                names.Append(new FixedString32Bytes(","));
            names.Append(new FixedString64Bytes("Value"));
            ++nameCount;
            #endregion
        }
        #endif
    }
}