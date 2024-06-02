namespace BsaTroop53Editor.KeyWriter
{
    internal class Program
    {
        static int Main( string[] args )
        {
            if( args.Length != 1 )
            {
                Console.WriteLine( "Writes the base64 string of the generated key to a specified file." );
                Console.WriteLine( "Usage: <pathToOutputFile>" );
                return 1;
            }

            var generator = new KeyGenerator.KeyGenerator();
            File.WriteAllText( args[0], generator.Base64Key );

            return 0;
        }
    }
}
