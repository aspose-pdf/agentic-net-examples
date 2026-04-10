using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF file with read/write access to allow incremental updates.
        using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        using (Document doc = new Document(fs))
        {
            // Change the PDF version to 1.5 using the Convert method.
            // The conversion log file is optional; an empty temporary file is used here.
            string tempLog = Path.GetTempFileName();
            doc.Convert(tempLog, PdfFormat.v_1_5, ConvertErrorAction.Delete);

            // Save the document incrementally. Parameterless Save() performs an incremental update.
            doc.Save();

            // After incremental save, write the updated content back to the desired output file.
            // Since the original stream was opened on the input file, we need to copy it to a new file.
            fs.Position = 0; // rewind to the beginning of the stream
            using (FileStream outFs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                fs.CopyTo(outFs);
            }

            // Clean up the temporary log file.
            File.Delete(tempLog);
        }

        Console.WriteLine($"PDF version updated to 1.5 and saved with incremental update: {outputPath}");
    }
}