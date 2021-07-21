using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileSystemCopy.Objects
{
    public class ProgressCalculator
    {
        float _initialValue = 0;
        public void Reset()
        {
            _initialValue = 0;
        }
        public void Append(float appendInitialValue)
        {
            if (appendInitialValue < 0f || appendInitialValue > 1f)
                throw new Exception("Append value is invalid");
            _initialValue += appendInitialValue;
        }
        public float Calculate(int current, int total)
        {
            if (current > total)
                throw new Exception("Current value cannot be higher than total");
            float maxAvailableValue = 1f - _initialValue;
            float currentValue = maxAvailableValue * current / (float)total;
            float sum = _initialValue + currentValue;
            if (sum > 1f)
                sum = 1f;
            return sum;
        }
    }
}
