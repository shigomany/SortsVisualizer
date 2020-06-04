using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SortsVisualizer.Algorithms
{
    public class HeapSort : ISort
    {
        int DelayMS = 1;
        Action StateChange;
        bool StopVal = false;
        TimeSpan _time;
        public TimeSpan Time { get => _time; set => _time = value; }

        private int heapSize;

        private void BuildHeap(List<SortElement> arr)
        {
            heapSize = arr.Count - 1;
            for (int i = heapSize / 2; i >= 0; i--)
            {
                if (StopVal)
                    return;
                Heapify(arr, i);
            }
        }

        private void Swap(List<SortElement> arr, int x, int y)//function to swap elements
        {
            var temp = arr[x];
            arr[x] = arr[y];
            arr[y] = temp;
            StateChange();
        }
        private async Task Heapify(List<SortElement> arr, int index)
        {
            int left = 2 * index;
            int right = 2 * index + 1;
            int largest = index;

            if (left <= heapSize && arr[left].Value > arr[index].Value)
                largest = left;

            if (right <= heapSize && arr[right].Value > arr[largest].Value)
                largest = right;

            if (largest != index)
            {
                Swap(arr, index, largest);
                await Heapify(arr, largest);
            }
        }
        public async Task InternalHeapSort(List<SortElement> arr)
        {
            BuildHeap(arr);
            for (int i = arr.Count - 1; i >= 0; i--)
            {
                if (StopVal)
                    return;
                Swap(arr, 0, i);
                await Task.Delay(DelayMS);
                heapSize--;
                await Heapify(arr, 0);
            }
        }

        public async void Sort(List<SortElement> data, Action StateChange)
        {
            this.StateChange = StateChange;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await InternalHeapSort(data);
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
