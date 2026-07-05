using System;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;
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

        // Document must be disposed via using (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // If there are no embedded files, report and exit
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            // Iterate over each attachment using reflection to avoid direct dependency on EmbeddedFile type
            foreach (var embedded in doc.EmbeddedFiles)
            {
                // Retrieve the attachment name via reflection
                string name = GetPropertyValue<string>(embedded, "Name") ?? "Unnamed";

                // Load attachment data into a memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Try to invoke Save(Stream) method
                    MethodInfo saveMethod = embedded.GetType().GetMethod("Save", new[] { typeof(Stream) });
                    if (saveMethod != null)
                    {
                        saveMethod.Invoke(embedded, new object[] { ms });
                    }
                    else
                    {
                        // Fallback to Save(string) overload if available
                        MethodInfo saveString = embedded.GetType().GetMethod("Save", new[] { typeof(string) });
                        if (saveString != null)
                        {
                            string tempPath = Path.GetTempFileName();
                            saveString.Invoke(embedded, new object[] { tempPath });
                            ms.Write(File.ReadAllBytes(tempPath), 0, (int)new FileInfo(tempPath).Length);
                            File.Delete(tempPath);
                        }
                        else
                        {
                            Console.WriteLine($"Cannot extract attachment: {name}");
                            continue;
                        }
                    }

                    byte[] data = ms.ToArray();

                    // Compute SHA‑256 hash of the attachment bytes
                    byte[] hash;
                    using (SHA256 sha = SHA256.Create())
                    {
                        hash = sha.ComputeHash(data);
                    }

                    // Convert hash to a hex string for display
                    string hashHex = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                    Console.WriteLine($"Attachment: {name}");
                    Console.WriteLine($"SHA‑256: {hashHex}");
                }
            }
        }
    }

    private static T GetPropertyValue<T>(object obj, string propertyName)
    {
        PropertyInfo prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (prop != null && prop.CanRead)
        {
            return (T)prop.GetValue(obj);
        }
        return default;
    }
}