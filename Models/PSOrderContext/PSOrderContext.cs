using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demo1.Models.PSOrderContext
{
    public partial class PSOrderContext : DbContext
    {
        public PSOrderContext()
        {
        }

        public PSOrderContext(DbContextOptions<PSOrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DeliveryNote> DeliveryNotes { get; set; } = null!;
        public virtual DbSet<KeyDoc> KeyDocs { get; set; } = null!;
        public virtual DbSet<MT_GroupSection> MT_GroupSections { get; set; } = null!;
        public virtual DbSet<MT_TypeInOut> MT_TypeInOuts { get; set; } = null!;
        public virtual DbSet<MT_TypeMail> MT_TypeMails { get; set; } = null!;
        public virtual DbSet<Normal> Normals { get; set; } = null!;
        public virtual DbSet<Return_Table> Return_Tables { get; set; } = null!;
        public virtual DbSet<Round> Rounds { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Type_Mail> Type_Mails { get; set; } = null!;
        public virtual DbSet<Usermain> Usermains { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=NB1911015;User ID=sa; database=PSOrder;Password=Password1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryNote>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DeliveryNote");

                entity.Property(e => e.ID)
                    .HasColumnType("decimal(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lot)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Date)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_DeliNote).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Re_Div)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_DivName)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Recipient)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_time)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserID)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<KeyDoc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("KeyDoc");

                entity.Property(e => e.Datetime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DivCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ID).ValueGeneratedOnAdd();

                entity.Property(e => e.Num).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Pay).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Period)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<MT_GroupSection>(entity =>
            {
                entity.HasKey(e => e.nGroupSectionID);

                entity.ToTable("MT_GroupSection");

                entity.Property(e => e.nGroupSectionID).ValueGeneratedNever();

                entity.Property(e => e.cActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.sGroupSectionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MT_TypeInOut>(entity =>
            {
                entity.HasKey(e => e.nID);

                entity.ToTable("MT_TypeInOut");

                entity.Property(e => e.nID).ValueGeneratedNever();

                entity.Property(e => e.cActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.dCreateDate).HasColumnType("datetime");

                entity.Property(e => e.dUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.sCreate)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.sTypeInOutName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.sUpdate)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MT_TypeMail>(entity =>
            {
                entity.HasKey(e => e.nID);

                entity.ToTable("MT_TypeMail");

                entity.Property(e => e.nID).ValueGeneratedNever();

                entity.Property(e => e.cActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.dCreateDate).HasColumnType("datetime");

                entity.Property(e => e.dUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.sCreate)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.sTypeMailName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.sUpdate)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Normal>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PK_ID")
                    .IsClustered(false);

                entity.ToTable("Normal");

                entity.Property(e => e.ID)
                    .HasColumnType("decimal(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Datetime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DivName)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EndT)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Lot)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Num).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Pay).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Period)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Product)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RC)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ref)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Res)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Sender)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TypeLetter)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TypeOfLetter)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserID)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.dDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Return_Table>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Return_Table");

                entity.Property(e => e.ID)
                    .HasColumnType("decimal(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lot)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Barcode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Cause)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Count).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Re_Country)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Date)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_DeliNote)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Div)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_DivName)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Group)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Province)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Reci)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Ref)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Sender)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Re_time)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserID)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Round");

                entity.Property(e => e.DayTrip)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ID).ValueGeneratedOnAdd();

                entity.Property(e => e.Period)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Round1).HasColumnName("Round");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.SectionCode);

                entity.ToTable("Section");

                entity.Property(e => e.SectionCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SectionGroup)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("MT_GroupSection => กลุ่มงาน : 1=สาขา,4=ส่วน/ฝ่าย/สำนัก,5=ต่างประเทศ,6=กองทุน");

                entity.Property(e => e.SectionName)
                    .HasMaxLength(90)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.dCreateDate).HasColumnType("datetime");

                entity.Property(e => e.dUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.sCreate)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.sUpdate)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Type_Mail>(entity =>
            {
                entity.ToTable("Type_Mail");

                entity.Property(e => e.ID)
                    .HasColumnType("decimal(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Type_InOut)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("MT_TypeInOut => 1 : ไปรษณียภัณฑ์ในประเทศ,2 : ไปรษณียภัณฑ์ต่างประเทศ");

                entity.Property(e => e.Type_Mail1)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Type_Mail")
                    .IsFixedLength()
                    .HasComment("MT_TypeMail => 0 : จดหมายธรรมดา,1 : จดหมายลงทะเบียน");

                entity.Property(e => e.Type_Name)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Type_Pay).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.dCreateDate).HasColumnType("datetime");

                entity.Property(e => e.dUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.sCreate)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.sUpdate)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usermain>(entity =>
            {
                entity.HasKey(e => e.OAUserID);

                entity.ToTable("Usermain");

                entity.Property(e => e.OAUserID)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Dep)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.No)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.dCreateDate).HasColumnType("datetime");

                entity.Property(e => e.dEndDate).HasColumnType("datetime");

                entity.Property(e => e.dStartDate).HasColumnType("datetime");

                entity.Property(e => e.dUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.sCreate)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.sUpdate)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
