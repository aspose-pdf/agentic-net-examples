using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDFs with their original owner password and the new owner password to set.
        var files = new[]
        {
            new { Input = "input1.pdf", Output = "output1.pdf", OwnerPassword = "oldOwner1", NewOwnerPassword = "newOwner1" },
            new { Input = "input2.pdf", Output = "output2.pdf", OwnerPassword = "oldOwner2", NewOwnerPassword = "newOwner2" }
            // Add more entries as needed.
        };

        foreach (var f in files)
        {
            if (!File.Exists(f.Input))
            {
                Console.Error.WriteLine($"File not found: {f.Input}");
                continue;
            }

            // Initialize the PdfFileSecurity facade.
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Load the source PDF.
                security.BindPdf(f.Input);

                // Change only the owner password; pass null for newUserPassword to keep the existing user password.
                bool success = security.ChangePassword(f.OwnerPassword, null, f.NewOwnerPassword);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to change password for {f.Input}");
                    continue;
                }

                // Save the updated PDF to the output path.
                security.Save(f.Output);
            }

            Console.WriteLine($"Processed: {f.Input} → {f.Output}");
        }
    }
}