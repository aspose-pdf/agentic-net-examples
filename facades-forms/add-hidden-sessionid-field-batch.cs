using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace AddHiddenSessionIdFieldBatch
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create sample PDF files (self‑contained example)
            string[] sampleFiles = new string[2];
            int sampleIndex = 0;
            while (sampleIndex < sampleFiles.Length)
            {
                string sampleFileName = "input" + (sampleIndex + 1).ToString() + ".pdf";
                using (Document sampleDoc = new Document())
                {
                    // Add a blank page (required for a form field)
                    sampleDoc.Pages.Add();
                    sampleDoc.Save(sampleFileName);
                }
                sampleFiles[sampleIndex] = sampleFileName;
                sampleIndex++;
            }

            // Step 2: Process each PDF and add a hidden SessionId field with a GUID value
            foreach (string pdfPath in sampleFiles)
            {
                using (Document doc = new Document(pdfPath))
                {
                    // Create a hidden text box field named "SessionId"
                    Rectangle fieldRect = new Rectangle(0, 0, 0, 0); // zero‑size rectangle makes the field invisible
                    TextBoxField sessionField = new TextBoxField(doc.Pages[1], fieldRect);
                    sessionField.PartialName = "SessionId";
                    sessionField.Value = Guid.NewGuid().ToString();

                    // Add the field to the document's form
                    doc.Form.Add(sessionField);

                    // Save the modified PDF
                    string outputFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_output.pdf";
                    doc.Save(outputFileName);
                }
            }
        }
    }
}
