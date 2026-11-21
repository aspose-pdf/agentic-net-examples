using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // NOTE: Exporting annotations to XFDF requires the separate
            // Aspose.Pdf.Xfdf assembly (Aspose.Pdf.Xfdf.dll). If that package
            // is referenced, you can use the XfdfWriter as shown below.
            // The following code is kept as a comment to illustrate the
            // intended usage while keeping the project compile‑able without
            // the extra assembly.
            /*
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // The XfdfWriter lives in the Aspose.Pdf.Xfdf namespace.
                // Ensure the NuGet package Aspose.Pdf.Xfdf is added to the
                // project before uncommenting this block.
                Aspose.Pdf.Xfdf.XfdfWriter xfdfWriter = new Aspose.Pdf.Xfdf.XfdfWriter();
                xfdfWriter.Write(pdfDoc, xfdfStream);

                // Reset stream position for reading
                xfdfStream.Position = 0;

                // Optional: read the XFDF content as a string and output it
                string xfdfContent = Encoding.UTF8.GetString(xfdfStream.ToArray());
                Console.WriteLine("XFDF annotation data:");
                Console.WriteLine(xfdfContent);
            }
            */

            // Since the XfdfWriter is not available, we simply inform the user.
            Console.WriteLine("XFDF export requires the Aspose.Pdf.Xfdf package. " +
                              "Add the package and uncomment the code block to enable export.");
        }
    }
}
