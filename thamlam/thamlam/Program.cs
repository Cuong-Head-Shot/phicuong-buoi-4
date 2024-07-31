using System;
using System.Collections.Generic;
using System.Linq;

namespace FractionalKnapsack
{
    // Định nghĩa lớp Item để lưu trữ thông tin của từng món đồ
    public class Item
    {
        public int Value { get; set; }
        public int Weight { get; set; }

        public Item(int value, int weight)
        {
            Value = value;
            Weight = weight;
        }

        public double Ratio
        {
            get { return (double)Value / Weight; }
        }
    }

    // Định nghĩa lớp SelectedItem để lưu trữ thông tin món đồ đã chọn và phân số
    public class SelectedItem
    {
        public Item Item { get; set; }
        public double Fraction { get; set; }

        public SelectedItem(Item item, double fraction)
        {
            Item = item;
            Fraction = fraction;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Khởi tạo danh sách các món đồ
            var items = new List<Item>
            {
                new Item(50, 10),
                new Item(30, 20),
                new Item(40, 30)
            };

            // Nhập dung tích của ba lô từ người dùng
            Console.Write("Enter the capacity of the Knapsack: ");
            int capacity = int.Parse(Console.ReadLine());

            // Gọi hàm GreedyKnapsack để tính toán giá trị tối đa và các món đồ đã chọn
            var result = GreedyKnapsack(capacity, items);
            double maxValue = result.Item1;
            List<SelectedItem> selectedItems = result.Item2;

            // Hiển thị kết quả
            Console.WriteLine("Maximum value in Knapsack = " + maxValue);
            Console.ReadLine();


        }

        // Hàm giải thuật tham lam cho bài toán ba lô phân số
        public static Tuple<double, List<SelectedItem>> GreedyKnapsack(int capacity, List<Item> items)
        {
            // Sắp xếp các món đồ theo tỷ lệ giá trị trên trọng lượng giảm dần
            items = items.OrderByDescending(item => item.Ratio).ToList();

            double totalValue = 0;
            int currentWeight = 0;
            var selectedItems = new List<SelectedItem>();

            foreach (var item in items)
            {
                if (currentWeight + item.Weight <= capacity)
                {
                    // Thêm toàn bộ món đồ vào ba lô
                    currentWeight += item.Weight;
                    totalValue += item.Value;
                    selectedItems.Add(new SelectedItem(item, 1.0));
                }
                else
                {
                    // Thêm một phần của món đồ vào ba lô
                    int remainingWeight = capacity - currentWeight;
                    double fraction = (double)remainingWeight / item.Weight;
                    totalValue += item.Value * fraction;
                    selectedItems.Add(new SelectedItem(new Item((int)(item.Value * fraction), remainingWeight), fraction));
                    break; // Đã đầy ba lô
                }
            }

            return Tuple.Create(totalValue, selectedItems);
        }
    }
}


