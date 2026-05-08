using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the "Score" field
        const string outputPdf = "output.pdf";  // PDF after applying the restrictions

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works with AcroForm fields. It implements IDisposable,
        // so we wrap it in a using block to ensure proper resource cleanup.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // The NumberField class (Aspose.Pdf.Forms) already restricts input to digits
            // (AllowedChars defaults to "0123456789"). To further limit the value to
            // a maximum of three characters (0‑100 fits in three digits), set the field limit.
            // This does not allow more than three characters to be entered.
            bool limitSet = formEditor.SetFieldLimit("Score", 3);
            if (!limitSet)
            {
                Console.Error.WriteLine("Failed to set field limit for 'Score'.");
            }

            // Save the modified document. FormEditor.Save() writes to the output file
            // specified in the constructor.
            formEditor.Save();
        }

        Console.WriteLine($"Field 'Score' configured and saved to '{outputPdf}'.");
    }
}