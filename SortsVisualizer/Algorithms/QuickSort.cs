using ImageMagick;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SortsVisualizer.Algorithms
{
    public class QS : ISort
    {
        const int Threshold = 2048;
        const int DelayMS = 5;
        public Action StateChange { get; set; }
        TimeSpan _time;
        public TimeSpan Time { get => _time; set => _time = value; }

        bool StopVal = false;
        public void Swap<T>(List<T> arr, int i, int j)
        {
            // Swap two element in an array with given indexes.
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
            StateChange();
        }

        public async Task QuickSort(List<SortElement> arr, int begin, int end)
        {
            int pivot = arr[(begin + (end - begin) / 2)].Value;
            int left = begin;
            int right = end;

            while (left <= right && !StopVal)
            {
                while (arr[left].Value < pivot && !StopVal)
                    left++;
                
                while (arr[right].Value > pivot && !StopVal)
                    right--;

                if (left <= right)
                {
                    Swap(arr, left, right);

                    await Task.Delay(TimeSpan.FromMilliseconds(DelayMS));
                    left++;
                    right--;
                }
            }

            if (begin < right && !StopVal)
                await QuickSort(arr, begin, left - 1);

            if (end > left && !StopVal)
                await QuickSort(arr, right + 1, end);
        }

        public async void Sort(List<SortElement> data, Action StateChange)
        {
            StopVal = false;
            this.StateChange = StateChange;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await QuickSort(data, 0, data.Count - 1);
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
