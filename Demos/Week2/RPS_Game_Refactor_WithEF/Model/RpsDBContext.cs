using Microsoft.EntityFrameworkCore;

namespace HelloWorldDemo.Model
{
    public class RpsDBContext: DbContext
    {
            public DbSet<Player> players {get; set;}
            public DbSet<Round> rounds {get; set;}
            public DbSet<Match> matches {get; set;}


            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // if( !optionsBuilder. )
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-0IVLTFI;Database=Rps12142020;Trusted_Connection=True");

                // string somestring = @"
                // some string

                // in new lines


                // ";
            }
        
    }
}