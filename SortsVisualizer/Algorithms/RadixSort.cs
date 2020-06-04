using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SortsVisualizer.Algorithms
{
    public class RadixSort : ISort
    {
        int DelayMS = 1;
        bool StopVal = false;
        Action StateChange;
        object locker = new object();

        TimeSpan _time;
        public TimeSpan Time { get => _time; set => _time = value; }

        public async Task CountSort(List<SortElement> arr, int n, int exp)
        {
            SortElement[] output = new SortElement[n]; // output array  
            int i;
            int[] count = new int[10];

            // Store count of occurrences in count[]  
            for (i = 0; i < n; i++)
            {
                if (StopVal)
                    return;
                count[(arr[i].Value / exp) % 10]++;
            }

            // Change count[i] so that count[i] now contains actual  
            //  position of this digit in output[]  
            for (i = 1; i < 10; i++)
            {
                if (StopVal)
                    return;
                count[i] += count[i - 1];
            }

            // Build the output array  
            for (i = n - 1; i >= 0; i--)
            {
                if (StopVal)
                    return;
                output[count[(arr[i].Value / exp) % 10] - 1] = arr[i];
                count[(arr[i].Value / exp) % 10]--;
            }
            var iter = 0;
            // Copy the output array to arr[], so that arr[] now  
            foreach(var el in output)
            {
                if (StopVal)
                    return;
                SetVals(arr, el, iter++);
                await Task.Delay(DelayMS);
            }

        }

        private void SetVals(List<SortElement> arr, SortElement el, int i)
        {
            arr[i] = el;
            StateChange();
        }

        // The main function to that sorts arr[] of size n using   
        // Radix Sort  
        public async Task InternalRadixsort(List<SortElement> arr, int n)
        {
            // Find the maximum number to know number of digits  
            var m = arr.Max(x => x.Value);

            // Do counting sort for every digit. Note that instead  
            // of passing digit number, exp is passed. exp is 10^i  
            // where i is current digit number  
            for (int exp = 1; m / exp > 0; exp *= 10)
                await CountSort(arr, n, exp);
        }


        public async void Sort(List<SortElement> data, Action StateChange)
        {
            StopVal = false;
            this.StateChange = StateChange;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await InternalRadixsort(data, data.Count);
            sw.Stop();
            Time = sw.Elapsed;
            StateChange();
        }

        public void Stop()
        {
            StopVal = true;
        }
    }
}
