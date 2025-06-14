using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CarSegmentPredict
{
    public class CarSegmentPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedSegmentation { get; set; }

        [ColumnName("Score")]
        public float[] Score { get; set; }
    }

    public partial class MainForm : Form
    {
        private readonly MLContext _mlContext;
        private ITransformer _trainedModel;
        private PredictionEngine<CarSegmentInput, CarSegmentPrediction> _predictionEngine;

        // Form controls
        private ComboBox cmbGender;
        private ComboBox cmbMarried;
        private NumericUpDown nudAge;
        private ComboBox cmbGraduated;
        private ComboBox cmbProfession;
        private NumericUpDown nudWorkExperience;
        private ComboBox cmbSpendingScore;
        private NumericUpDown nudFamilySize;
        private Button btnPredict;
        private Button btnTrainModel;
        private Label lblResult;
        private Label lblConfidence;
        private TextBox txtModelStatus;
        private ProgressBar progressBar;

        
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Car Segment Predictor";
            this.Size = new Size(500, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            int yPosition = 20;
            int labelWidth = 120;
            int controlWidth = 200;
            int spacing = 35;

            // Gender
            var lblGender = new Label { Text = "Gender:", Location = new Point(20, yPosition), Size = new Size(labelWidth, 23) };
            cmbGender = new ComboBox { Location = new Point(150, yPosition), Size = new Size(controlWidth, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGender.Items.AddRange(new[] { "Male", "Female" });
            this.Controls.AddRange(new Control[] { lblGender, cmbGender });
            yPosition += spacing;

            // Married
            var lblMarried = new Label { Text = "Married:", Location = new Point(20, yPosition), Size = new Size(labelWidth, 23) };
            cmbMarried = new ComboBox { Location = new Point(150, yPosition), Size = new Size(controlWidth, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbMarried.Items.AddRange(new[] { "Yes", "No" });
            this.Controls.AddRange(new Control[] { lblMarried, cmbMarried });
            yPosition += spacing;

            // Age
            var lblAge = new Label { Text = "Age:", Location = new Point(20, yPosition), Size = new Size(labelWidth, 23) };
            nudAge = new NumericUpDown { Location = new Point(150, yPosition), Size = new Size(controlWidth, 23), Minimum = 18, Maximum = 80, Value = 30 };
            this.Controls.AddRange(new Control[] { lblAge, nudAge });
            yPosition += spacing;

            // Graduated
            var lblGraduated = new Label { Text = "Graduated:", Location = new Point(20, yPosition), Size = new Size(labelWidth, 23) };
            cmbGraduated = new ComboBox { Location = new Point(150, yPosition), Size = new Size(controlWidth, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGraduated.Items.AddRange(new[] { "Yes", "No" });
            this.Controls.AddRange(new Control[] { lblGraduated, cmbGraduated });
            yPosition += spacing;

            // Profession
            var lblProfession = new Label { Text = "Profession:", Location = new Point(20, yPosition), Size = new Size(labelWidth, 23) };
            cmbProfession = new ComboBox { Location = new Point(150, yPosition), Size = new Size(controlWidth, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbProfession.Items.AddRange(new[] { "Healthcare", "Engineer", "Lawyer", "Artist", "Doctor", "Homemaker", "Entertainment", "Marketing", "Executive" });
            this.Controls.AddRange(new Control[] { lblProfession, cmbProfession });
            yPosition += spacing;

            // Work Experience
            var lblWorkExp = new Label { Text = "Work Experience:", Location = new Point(20, yPosition), Size = new Size(labelWidth, 23) };
            nudWorkExperience = new NumericUpDown { Location = new Point(150, yPosition), Size = new Size(controlWidth, 23), Minimum = 0, Maximum = 50, Value = 5 };
            this.Controls.AddRange(new Control[] { lblWorkExp, nudWorkExperience });
            yPosition += spacing;

            // Spending Score
            var lblSpendingScore = new Label { Text = "Spending Score:", Location = new Point(20, yPosition), Size = new Size(labelWidth, 23) };
            cmbSpendingScore = new ComboBox { Location = new Point(150, yPosition), Size = new Size(controlWidth, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbSpendingScore.Items.AddRange(new[] { "Low", "Average", "High" });
            this.Controls.AddRange(new Control[] { lblSpendingScore, cmbSpendingScore });
            yPosition += spacing;

            // Family Size
            var lblFamilySize = new Label { Text = "Family Size:", Location = new Point(20, yPosition), Size = new Size(labelWidth, 23) };
            nudFamilySize = new NumericUpDown { Location = new Point(150, yPosition), Size = new Size(controlWidth, 23), Minimum = 1, Maximum = 10, Value = 3 };
            this.Controls.AddRange(new Control[] { lblFamilySize, nudFamilySize });
            yPosition += spacing + 10;

            // Buttons
            btnTrainModel = new Button { Text = "Train Model", Location = new Point(20, yPosition), Size = new Size(120, 30) };
            btnTrainModel.Click += BtnTrainModel_Click;
            btnPredict = new Button { Text = "Predict Segment", Location = new Point(150, yPosition), Size = new Size(120, 30), Enabled = false };
            btnPredict.Click += BtnPredict_Click;
            this.Controls.AddRange(new Control[] { btnTrainModel, btnPredict });
            yPosition += 40;

            // Progress Bar
            progressBar = new ProgressBar { Location = new Point(20, yPosition), Size = new Size(330, 23), Style = ProgressBarStyle.Marquee, Visible = false };
            this.Controls.Add(progressBar);
            yPosition += 35;

            // Model Status
            var lblModelStatus = new Label { Text = "Model Status:", Location = new Point(20, yPosition), Size = new Size(100, 23) };
            txtModelStatus = new TextBox { Location = new Point(20, yPosition + 25), Size = new Size(430, 60), Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };
            txtModelStatus.Text = "Model not trained. Click 'Train Model' to start.";
            this.Controls.AddRange(new Control[] { lblModelStatus, txtModelStatus });
            yPosition += 90;

            // Results
            var lblResultTitle = new Label { Text = "Prediction Result:", Location = new Point(20, yPosition), Size = new Size(120, 23), Font = new Font("Arial", 9, FontStyle.Bold) };
            yPosition += 25;
            lblResult = new Label { Location = new Point(20, yPosition), Size = new Size(430, 30), Font = new Font("Arial", 12, FontStyle.Bold), ForeColor = Color.Blue };
            yPosition += 35;
            lblConfidence = new Label { Location = new Point(20, yPosition), Size = new Size(430, 23), Font = new Font("Arial", 9) };
            this.Controls.AddRange(new Control[] { lblResultTitle, lblResult, lblConfidence });

            this.ResumeLayout(false);
        }

        private void LoadDefaultValues()
        {
            cmbGender.SelectedIndex = 0;
            cmbMarried.SelectedIndex = 0;
            cmbGraduated.SelectedIndex = 0;
            cmbProfession.SelectedIndex = 0;
            cmbSpendingScore.SelectedIndex = 1;
        }

        private async void BtnTrainModel_Click(object sender, EventArgs e)
        {
            string dataPath = "C:\\Users\\Aslı\\source\\repos\\CarSegmentPredict\\carSegmentTrain.csv";

            if (!File.Exists(dataPath))
            {
                MessageBox.Show($"Training data file '{dataPath}' not found. Please ensure the file is in the application directory.",
                    "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnTrainModel.Enabled = false;
            btnPredict.Enabled = false;
            progressBar.Visible = true;
            txtModelStatus.Text = "Training model, please wait...";

            try
            {
                await System.Threading.Tasks.Task.Run(() => TrainModel(dataPath));

                txtModelStatus.Text = "Model trained successfully! You can now make predictions.";
                btnPredict.Enabled = true;
            }
            catch (Exception ex)
            {
                txtModelStatus.Text = $"Error training model: {ex.Message}";
                MessageBox.Show($"Error training model: {ex.Message}", "Training Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar.Visible = false;
                btnTrainModel.Enabled = true;
            }
        }

        private void TrainModel(string dataPath)
        {
            // Load data
            IDataView dataView = _mlContext.Data.LoadFromTextFile<CarSegmentData>(
            path: dataPath,
            hasHeader: true,
            separatorChar: ',');

            // Split data
            var dataSplit = _mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

            // Create pipeline
            var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding(
                    outputColumnName: "GenderEncoded",
                    inputColumnName: nameof(CarSegmentData.Gender))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(
                    outputColumnName: "MarriedEncoded",
                    inputColumnName: nameof(CarSegmentData.Married)))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(
                    outputColumnName: "GraduatedEncoded",
                    inputColumnName: nameof(CarSegmentData.Graduated)))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(
                    outputColumnName: "ProfessionEncoded",
                    inputColumnName: nameof(CarSegmentData.Profession)))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(
                    outputColumnName: "SpendingScoreEncoded",
                    inputColumnName: nameof(CarSegmentData.SpendingScore)))
                .Append(_mlContext.Transforms.Concatenate("Features",
                    "GenderEncoded", "MarriedEncoded", nameof(CarSegmentData.Age),
                    "GraduatedEncoded", "ProfessionEncoded", nameof(CarSegmentData.WorkExperience),
                    "SpendingScoreEncoded", nameof(CarSegmentData.FamilySize)))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "Label",
                    inputColumnName: nameof(CarSegmentData.Segmentation)))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(
                    labelColumnName: "Label",
                    featureColumnName: "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue(
                    outputColumnName: "PredictedLabel",
                    inputColumnName: "PredictedLabel"));

            // Train model
            //print row count
            long a = dataSplit.TrainSet.GetRowCount() ?? 0;
          
            _trainedModel = pipeline.Fit(dataSplit.TrainSet);

            // Evaluate model
            var predictions = _trainedModel.Transform(dataSplit.TestSet);
            var metrics = _mlContext.MulticlassClassification.Evaluate(predictions, "Label");

            // Update UI with metrics
            this.Invoke(new Action(() =>
            {
                txtModelStatus.Text += $"\r\n\r\nModel Quality Metrics:\r\n";
                txtModelStatus.Text += $"Accuracy: {metrics.MacroAccuracy:P2}\r\n";
                txtModelStatus.Text += $"Log Loss: {metrics.LogLoss:F2}";
            }));

            // Create prediction engine
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<CarSegmentInput, CarSegmentPrediction>(_trainedModel);

            // Save model
            string modelPath = "car_segment_model.zip";
            _mlContext.Model.Save(_trainedModel, null, modelPath);
        }

        private void BtnPredict_Click(object sender, EventArgs e)
        {
            if (_predictionEngine == null)
            {
                MessageBox.Show("Please train the model first.", "Model Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var input = new CarSegmentInput
                {
                    Gender = cmbGender.SelectedItem?.ToString(),
                    Married = cmbMarried.SelectedItem?.ToString(),
                    Age = (float)nudAge.Value,
                    Graduated = cmbGraduated.SelectedItem?.ToString(),
                    Profession = cmbProfession.SelectedItem?.ToString(),
                    WorkExperience = (float)nudWorkExperience.Value,
                    SpendingScore = cmbSpendingScore.SelectedItem?.ToString(),
                    FamilySize = (float)nudFamilySize.Value
                };

                var prediction = _predictionEngine.Predict(input);

                lblResult.Text = $"Predicted Car Segment: {prediction.PredictedSegmentation}";

                var maxScore = prediction.Score.Max();
                var confidence = maxScore * 100;
                lblConfidence.Text = $"Confidence: {confidence:F1}%";

                // Change color based on confidence
                if (confidence > 70)
                    lblResult.ForeColor = Color.Green;
                else if (confidence > 50)
                    lblResult.ForeColor = Color.Orange;
                else
                    lblResult.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error making prediction: {ex.Message}", "Prediction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}