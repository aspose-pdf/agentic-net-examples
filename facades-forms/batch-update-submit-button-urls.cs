using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputFolder = "InputPdfs";
        const string outputFolder = "OutputPdfs";
        const string newSubmitUrl = "https://new.example.com/submit";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    if (doc.Form != null && doc.Form.Fields != null && doc.Form.Fields.Length > 0)
                    {
                        foreach (Field field in doc.Form.Fields)
                        {
                            if (field is ButtonField button)
                            {
                                // Create a SubmitFormAction and assign the new URL
                                var submit = new SubmitFormAction();
                                submit.Url = new FileSpecification(newSubmitUrl);
                                button.OnActivated = submit;
                            }
                        }
                    }

                    doc.Save(outputPath);
                }

                Console.WriteLine($"Successfully updated: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}
