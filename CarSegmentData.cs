using Microsoft.ML.Data;

namespace CarSegmentPredict
{
    internal class CarSegmentData
    {
        [LoadColumn(0)]
        public string ID { get; set; }

        [LoadColumn(1)]
        public string Gender { get; set; }

        [LoadColumn(2)]
        public string Married { get; set; }

        [LoadColumn(3)]
        public float Age { get; set; }

        [LoadColumn(4)]
        public string Graduated { get; set; }

        [LoadColumn(5)]
        public string Profession { get; set; }

        [LoadColumn(6)]
        public float WorkExperience { get; set; }

        [LoadColumn(7)]
        public string SpendingScore { get; set; }

        [LoadColumn(8)]
        public float FamilySize { get; set; }

        [LoadColumn(9)]
        public string Category { get; set; }

        [LoadColumn(10)]
        public string Segmentation { get; set; }
    }
}