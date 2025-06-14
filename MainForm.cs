using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarSegmentPredict
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            _mlContext = new MLContext(seed: 0);
            InitializeComponent();
            LoadDefaultValues();
        }
    }
}
