using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Step 1: Create sample PDFs with a text box field (self‑contained example)
        for (int i = 1; i <= 2; i++)
        {
            string sampleFileName = "doc" + i + ".pdf";
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                // Rectangle for the text box field (left, bottom, right, top)
                Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
                TextBoxField textBox = new TextBoxField(sampleDoc.Pages[1], fieldRect);
                textBox.PartialName = "SampleField" + i;
                textBox.Value = "Sample Text";
                sampleDoc.Form.Add(textBox);
                sampleDoc.Save(sampleFileName);
            }
        }

        // Step 2: Load a custom font (using a standard font for the demo)
        Aspose.Pdf.Text.Font customFont = Aspose.Pdf.Text.FontRepository.FindFont("Arial");

        // Step 3: Collect up to four PDF files from the current folder
        IEnumerable<string> allFiles = Directory.EnumerateFiles(".", "doc*.pdf");
        List<string> pdfFiles = new List<string>();
        foreach (string f in allFiles)
        {
            if (pdfFiles.Count >= 4)
            {
                break;
            }
            pdfFiles.Add(f);
        }

        // Step 4: Apply the custom font to every text box field in each PDF
        for (int idx = 0; idx < pdfFiles.Count; idx++)
        {
            string inputPath = pdfFiles[idx];
            string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_updated.pdf";

            using (Document doc = new Document(inputPath))
            {
                foreach (Field field in doc.Form.Fields)
                {
                    if (field is TextBoxField)
                    {
                        TextBoxField tbField = (TextBoxField)field;
                        // DefaultAppearance constructor requires font name, size and a System.Drawing.Color
                        tbField.DefaultAppearance = new DefaultAppearance(customFont.FontName, 12.0, System.Drawing.Color.Black);
                    }
                }
                doc.Save(outputFileName);
            }
        }
    }
}
