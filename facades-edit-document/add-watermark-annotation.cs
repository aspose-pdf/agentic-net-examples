using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class WatermarkConfig
{
    public string InputFolder { get; set; }
    public string OutputFolder { get; set; }
    public string Text { get; set; }
    public Aspose.Pdf.Color Color { get; set; }
    public float Opacity { get; set; }
}

public class Program
{
    public static void Main()
    {
        WatermarkConfig config = new WatermarkConfig();
        config.InputFolder = "InputPdfs";
        config.OutputFolder = "OutputPdfs";
        config.Text = "CONFIDENTIAL";
        config.Color = Aspose.Pdf.Color.Red;
        config.Opacity = 0.3f;

        if (!Directory.Exists(config.InputFolder))
        {
            Console.Error.WriteLine("Input folder not found: " + config.InputFolder);
            return;
        }

        if (!Directory.Exists(config.OutputFolder))
        {
            Directory.CreateDirectory(config.OutputFolder);
        }

        // Change current directory to the output folder so that Save uses a simple filename.
        Directory.SetCurrentDirectory(config.OutputFolder);

        string[] pdfFiles = Directory.GetFiles(config.InputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputFileName = fileNameWithoutExt + "_watermarked.pdf";

            using (Document doc = new Document(pdfPath))
            {
                int pageCount = doc.Pages.Count;
                for (int i = 1; i <= pageCount; i++)
                {
                    Page page = doc.Pages[i];
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);
                    WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);
                    watermark.Contents = config.Text;
                    watermark.Color = config.Color;
                    watermark.Opacity = config.Opacity;
                    page.Annotations.Add(watermark);
                }

                doc.Save(outputFileName);
                Console.WriteLine("Saved watermarked PDF: " + outputFileName);
            }
        }
    }
}
