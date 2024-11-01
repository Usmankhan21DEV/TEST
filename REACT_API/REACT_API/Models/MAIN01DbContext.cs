using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace REACT_API.Models;

public partial class MAIN01DbContext : DbContext
{
    public MAIN01DbContext()
    {
    }

    public MAIN01DbContext(DbContextOptions<MAIN01DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Aggregatedcounter> Aggregatedcounters { get; set; }

    public virtual DbSet<BackupImeiIpc> BackupImeiIpcs { get; set; }

    public virtual DbSet<CheckBalance4user> CheckBalance4users { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<Distributedlock> Distributedlocks { get; set; }

    public virtual DbSet<Hash> Hashes { get; set; }

    public virtual DbSet<ImeiUserJunction> ImeiUserJunctions { get; set; }

    public virtual DbSet<Info> Infos { get; set; }

    public virtual DbSet<InfoDelay> InfoDelays { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Jobparameter> Jobparameters { get; set; }

    public virtual DbSet<Jobqueue> Jobqueues { get; set; }

    public virtual DbSet<Jobstate> Jobstates { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<OrderHistory> OrderHistories { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PackageServiceJunction> PackageServiceJunctions { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Refund> Refunds { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<UserBalance> UserBalances { get; set; }

    public virtual DbSet<UserComment> UserComments { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Userbalanceandrefund> Userbalanceandrefunds { get; set; }

    public virtual DbSet<ViewUserbalance> ViewUserbalances { get; set; }

    public virtual DbSet<VwImeiUserCost> VwImeiUserCosts { get; set; }

    public virtual DbSet<VwOrderProgress> VwOrderProgresses { get; set; }

    public virtual DbSet<VwPaymentdetail> VwPaymentdetails { get; set; }

    public virtual DbSet<VwPsJuncionNew4mat> VwPsJuncionNew4mats { get; set; }

    public virtual DbSet<VwPsJunction> VwPsJunctions { get; set; }

    public virtual DbSet<VwRefund> VwRefunds { get; set; }

    public virtual DbSet<VwUserImei> VwUserImeis { get; set; }

    public virtual DbSet<VwUserInfo> VwUserInfos { get; set; }

    public virtual DbSet<VwUserPackage> VwUserPackages { get; set; }

    public virtual DbSet<VwUserPackageCost> VwUserPackageCosts { get; set; }

    public virtual DbSet<VwUserServiceCost> VwUserServiceCosts { get; set; }

    public virtual DbSet<VwUserbalanceNew4mat> VwUserbalanceNew4mats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=IMEI_INFO;user id=root;password=admin", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.39-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("activity_log");

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.Activity)
                .HasMaxLength(45)
                .HasColumnName("ACTIVITY");
            entity.Property(e => e.CreatedDate)
                .HasMaxLength(45)
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.FromUser)
                .HasMaxLength(45)
                .HasColumnName("FROM_USER");
            entity.Property(e => e.TableSeqNum)
                .HasMaxLength(45)
                .HasColumnName("TABLE_SEQ_NUM");
            entity.Property(e => e.UserSeqNum)
                .HasMaxLength(45)
                .HasColumnName("USER_SEQ_NUM");
        });

        modelBuilder.Entity<Aggregatedcounter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("aggregatedcounter")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Key, "IX_CounterAggregated_Key").IsUnique();

            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.Key).HasMaxLength(100);
        });

        modelBuilder.Entity<BackupImeiIpc>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("backup_imei_ipc");

            entity.HasIndex(e => e.Imei, "IMEI_UNIQUE").IsUnique();

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("ENTRY_DATE");
            entity.Property(e => e.Imei)
                .HasMaxLength(45)
                .HasColumnName("IMEI");
            entity.Property(e => e.IphoneCarrier)
                .HasColumnType("text")
                .HasColumnName("IPHONE CARRIER");
        });

        modelBuilder.Entity<CheckBalance4user>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("check_balance_4user");

            entity.Property(e => e.Balance)
                .HasPrecision(33, 2)
                .HasColumnName("BALANCE");
            entity.Property(e => e.FullName)
                .HasMaxLength(511)
                .HasDefaultValueSql("''")
                .HasColumnName("FULL_NAME");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("counter")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Key, "IX_Counter_Key");

            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.Key).HasMaxLength(100);
        });

        modelBuilder.Entity<Distributedlock>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("distributedlock")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.Resource).HasMaxLength(100);
        });

        modelBuilder.Entity<Hash>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("hash")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => new { e.Key, e.Field }, "IX_Hash_Key_Field").IsUnique();

            entity.Property(e => e.ExpireAt).HasMaxLength(6);
            entity.Property(e => e.Field).HasMaxLength(40);
            entity.Property(e => e.Key).HasMaxLength(100);
        });

        modelBuilder.Entity<ImeiUserJunction>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("imei_user_junction");

            entity.HasIndex(e => e.UserSeqNum, "imei_user_junction_ibfk_1");

            entity.HasIndex(e => e.InfoSeqNum, "imei_user_junction_ibfk_2");

            entity.HasIndex(e => e.InfoDSeqNum, "imei_user_junction_ibfk_2_idx");

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.InfoDSeqNum).HasColumnName("INFO_D_SEQ_NUM");
            entity.Property(e => e.InfoSeqNum).HasColumnName("INFO_SEQ_NUM");
            entity.Property(e => e.SearchDate)
                .HasColumnType("datetime")
                .HasColumnName("Search_DATE");
            entity.Property(e => e.ServiceFee)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("SERVICE_FEE");
            entity.Property(e => e.ServiceNo).HasColumnName("SERVICE_NO");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");

            entity.HasOne(d => d.InfoSeqNumNavigation).WithMany(p => p.ImeiUserJunctions)
                .HasForeignKey(d => d.InfoSeqNum)
                .HasConstraintName("imei_user_junction_ibfk_2");

            entity.HasOne(d => d.UserSeqNumNavigation).WithMany(p => p.ImeiUserJunctions)
                .HasForeignKey(d => d.UserSeqNum)
                .HasConstraintName("imei_user_junction_ibfk_1");
        });

        modelBuilder.Entity<Info>(entity =>
        {
            entity.HasKey(e => e.IdSeq).HasName("PRIMARY");

            entity.ToTable("info");

            entity.HasIndex(e => e.Imei, "IMEI").IsUnique();

            entity.Property(e => e.IdSeq).HasColumnName("ID_SEQ");
            entity.Property(e => e.AppleActivationStatusImeiSn)
                .HasColumnType("text")
                .HasColumnName("APPLE ACTIVATION STATUS - IMEI/SN");
            entity.Property(e => e.AppleMdmStatus)
                .HasColumnType("text")
                .HasColumnName("APPLE MDM STATUS");
            entity.Property(e => e.BrandModelInfo)
                .HasColumnType("text")
                .HasColumnName("BRAND & MODEL INFO");
            entity.Property(e => e.EntryDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("ENTRY_DATE");
            entity.Property(e => e.GooglePixelInfo)
                .HasColumnType("text")
                .HasColumnName("GOOGLE PIXEL INFO");
            entity.Property(e => e.IPhoneSimLock)
                .HasColumnType("text")
                .HasColumnName("iPHONE SIM-LOCK");
            entity.Property(e => e.Imei)
                .HasMaxLength(20)
                .HasColumnName("IMEI");
            entity.Property(e => e.IphoneCarrier)
                .HasColumnType("text")
                .HasColumnName("IPhone Carrier");
            entity.Property(e => e.MdmLockBypassIPhoneIPad)
                .HasColumnType("text")
                .HasColumnName("MDM LOCK BYPASS - iPHONE/iPAD");
            entity.Property(e => e.MotorolaInfo)
                .HasColumnType("text")
                .HasColumnName("MOTOROLA INFO");
            entity.Property(e => e.OneplusInfo)
                .HasColumnType("text")
                .HasColumnName("ONEPLUS INFO");
            entity.Property(e => e.SamsungInfo)
                .HasColumnType("text")
                .HasColumnName("SAMSUNG INFO");
            entity.Property(e => e.SamsungInfoPro)
                .HasColumnType("text")
                .HasColumnName("SAMSUNG INFO - PRO");
        });

        modelBuilder.Entity<InfoDelay>(entity =>
        {
            entity.HasKey(e => e.IdSeq).HasName("PRIMARY");

            entity.ToTable("info_delay");

            entity.Property(e => e.IdSeq).HasColumnName("ID_SEQ");
            entity.Property(e => e.AppleBasicInfo)
                .HasColumnType("text")
                .HasColumnName("APPLE BASIC INFO");
            entity.Property(e => e.AppleMdmStatus)
                .HasColumnType("text")
                .HasColumnName("APPLE MDM STATUS");
            entity.Property(e => e.ClaroAllCountriesPremiumIPhone14)
                .HasColumnType("text")
                .HasColumnName("CLARO ALL COUNTRIES - PREMIUM (iPHONE 14)");
            entity.Property(e => e.ClaroAllCountriesPremiumIPhone15)
                .HasColumnType("text")
                .HasColumnName("CLARO ALL COUNTRIES - PREMIUM (iPHONE 15)");
            entity.Property(e => e.ClaroAllCountriesPremiumUpToIPhone13)
                .HasColumnType("text")
                .HasColumnName("CLARO ALL COUNTRIES - PREMIUM (UP TO iPHONE 13)");
            entity.Property(e => e.EntryDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("ENTRY_DATE");
            entity.Property(e => e.ICloudOnOff)
                .HasColumnType("text")
                .HasColumnName("iCLOUD ON/OFF");
            entity.Property(e => e.IPhoneCarrierFmiBlacklist)
                .HasColumnType("text")
                .HasColumnName("iPHONE CARRIER - FMI & BLACKLIST");
            entity.Property(e => e.Imei)
                .HasMaxLength(20)
                .HasColumnName("IMEI");
            entity.Property(e => e.SamsungAtTCricketAllModels)
                .HasColumnType("text")
                .HasColumnName("SAMSUNG AT&T/CRICKET... ALL MODELS");
            entity.Property(e => e.UsaAtTCleanActiveLine)
                .HasColumnType("text")
                .HasColumnName("USA AT&T - CLEAN/ACTIVE LINE");
            entity.Property(e => e.UsaCricketClean6MonthsOld)
                .HasColumnType("text")
                .HasColumnName("USA CRICKET - CLEAN & 6 MONTHS OLD");
            entity.Property(e => e.UsaTMobileClean)
                .HasColumnType("text")
                .HasColumnName("USA T-MOBILE - CLEAN");
            entity.Property(e => e.WwBlacklistStatus)
                .HasColumnType("text")
                .HasColumnName("WW BLACKLIST STATUS");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("job")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.StateName, "IX_Job_StateName");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.ExpireAt).HasMaxLength(6);
            entity.Property(e => e.StateName).HasMaxLength(20);
        });

        modelBuilder.Entity<Jobparameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("jobparameter")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.JobId, "FK_JobParameter_Job");

            entity.HasIndex(e => new { e.JobId, e.Name }, "IX_JobParameter_JobId_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(40);

            entity.HasOne(d => d.Job).WithMany(p => p.Jobparameters)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_JobParameter_Job");
        });

        modelBuilder.Entity<Jobqueue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("jobqueue")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => new { e.Queue, e.FetchedAt }, "IX_JobQueue_QueueAndFetchedAt");

            entity.Property(e => e.FetchToken).HasMaxLength(36);
            entity.Property(e => e.FetchedAt).HasMaxLength(6);
            entity.Property(e => e.Queue).HasMaxLength(50);
        });

        modelBuilder.Entity<Jobstate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("jobstate")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.JobId, "FK_JobState_Job");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Reason).HasMaxLength(100);

            entity.HasOne(d => d.Job).WithMany(p => p.Jobstates)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_JobState_Job");
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("list")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.ExpireAt).HasMaxLength(6);
            entity.Property(e => e.Key).HasMaxLength(100);
        });

        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("order_history");

            entity.HasIndex(e => e.InfoSeqNum, "ORDER_HISTORY_ibfk_2_idx");

            entity.HasIndex(e => e.ServiceSeqNum, "ORDER_HISTORY_ibfk_3");

            entity.HasIndex(e => e.UserSeqNum, "USER_SEQ_NUM");

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.CompletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Completed_DATE");
            entity.Property(e => e.Imei)
                .HasMaxLength(45)
                .HasColumnName("IMEI");
            entity.Property(e => e.InfoSeqNum).HasColumnName("INFO_SEQ_NUM");
            entity.Property(e => e.OrderId)
                .HasMaxLength(45)
                .HasColumnName("ORDER_ID");
            entity.Property(e => e.RequestedEmail)
                .HasMaxLength(45)
                .HasColumnName("Requested_EMAIL");
            entity.Property(e => e.SearchDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("Search_DATE");
            entity.Property(e => e.ServiceCost)
                .HasPrecision(10, 2)
                .HasColumnName("SERVICE_COST");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(45)
                .HasColumnName("SERVICE_NAME");
            entity.Property(e => e.ServiceSeqNum).HasColumnName("SERVICE_SEQ_NUM");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");

            entity.HasOne(d => d.InfoSeqNumNavigation).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.InfoSeqNum)
                .HasConstraintName("ORDER_HISTORY_ibfk_2");

            entity.HasOne(d => d.ServiceSeqNumNavigation).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.ServiceSeqNum)
                .HasConstraintName("ORDER_HISTORY_ibfk_3");

            entity.HasOne(d => d.UserSeqNumNavigation).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.UserSeqNum)
                .HasConstraintName("ORDER_HISTORY_ibfk_1");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("packages");

            entity.HasIndex(e => e.SeqNum, "SEQ_NUM").IsUnique();

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<PackageServiceJunction>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("package_service_junction");

            entity.HasIndex(e => e.PackageSeqNum, "fk_package");

            entity.HasIndex(e => e.ServiceSeqNum, "fk_service");

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasColumnName("COST");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.PackageSeqNum).HasColumnName("PACKAGE_SEQ_NUM");
            entity.Property(e => e.ServiceSeqNum).HasColumnName("SERVICE_SEQ_NUM");

            entity.HasOne(d => d.PackageSeqNumNavigation).WithMany(p => p.PackageServiceJunctions)
                .HasForeignKey(d => d.PackageSeqNum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_package");

            entity.HasOne(d => d.ServiceSeqNumNavigation).WithMany(p => p.PackageServiceJunctions)
                .HasForeignKey(d => d.ServiceSeqNum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_service");
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("payment_detail");

            entity.HasIndex(e => e.UserSeqNum, "USER_SEQ_NUM");

            entity.HasIndex(e => e.PackageSeqNum, "fk_package_balance");

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("AMOUNT");
            entity.Property(e => e.BuyerEmail)
                .HasMaxLength(255)
                .HasColumnName("BUYER_EMAIL");
            entity.Property(e => e.BuyerId)
                .HasMaxLength(255)
                .HasColumnName("BUYER_ID");
            entity.Property(e => e.CreateDatetime)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATETIME");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.PackageSeqNum).HasColumnName("Package_SEQ_NUM");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(255)
                .HasColumnName("PAYMENT_METHOD");
            entity.Property(e => e.Status)
                .HasMaxLength(1000)
                .HasColumnName("STATUS");
            entity.Property(e => e.Tax)
                .HasMaxLength(255)
                .HasColumnName("TAX");
            entity.Property(e => e.TransId)
                .HasMaxLength(255)
                .HasColumnName("TRANS_ID");
            entity.Property(e => e.UpdateDatetime)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATETIME");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");

            entity.HasOne(d => d.PackageSeqNumNavigation).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.PackageSeqNum)
                .HasConstraintName("fk_package_balance");

            entity.HasOne(d => d.UserSeqNumNavigation).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.UserSeqNum)
                .HasConstraintName("payment_detail_ibfk_1");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("payment_types");

            entity.HasIndex(e => e.SeqNum, "PAYMENT_TYPES_PK").IsUnique();

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.EntryDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("ENTRY_DATE");
            entity.Property(e => e.PaymentTypeName)
                .HasMaxLength(50)
                .HasColumnName("PAYMENT_TYPE_NAME");
        });

        modelBuilder.Entity<Refund>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("refund");

            entity.HasIndex(e => e.UserSeqNum, "refund_foreign_key_1_idx");

            entity.HasIndex(e => e.ServiceNo, "refund_foreign_key_2_idx");

            entity.HasIndex(e => e.PackageSeqNum, "refund_foreign_key_2_idx1");

            entity.HasIndex(e => e.BalanceSeqNum, "refund_foreign_key_4_idx");

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.BalanceSeqNum).HasColumnName("BALANCE_SEQ_NUM");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.PackageSeqNum).HasColumnName("Package_SEQ_NUM");
            entity.Property(e => e.Reason).HasColumnType("text");
            entity.Property(e => e.ServiceNo).HasColumnName("Service_no");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");

            entity.HasOne(d => d.BalanceSeqNumNavigation).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.BalanceSeqNum)
                .HasConstraintName("refund_foreign_key_4");

            entity.HasOne(d => d.PackageSeqNumNavigation).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.PackageSeqNum)
                .HasConstraintName("refund_foreign_key_3");

            entity.HasOne(d => d.ServiceNoNavigation).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.ServiceNo)
                .HasConstraintName("refund_foreign_key_2");

            entity.HasOne(d => d.UserSeqNumNavigation).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.UserSeqNum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("refund_foreign_key_1");
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("server")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasMaxLength(100);
            entity.Property(e => e.LastHeartbeat).HasMaxLength(6);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("services");

            entity.HasIndex(e => e.ApiKey, "ApiKey_UNIQUE").IsUnique();

            entity.HasIndex(e => e.SeqNum, "SEQ_NUM").IsUnique();

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Delay)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NO'");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Service1)
                .HasMaxLength(255)
                .HasColumnName("Service");
            entity.Property(e => e.Type).HasMaxLength(45);
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("set")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => new { e.Key, e.Value }, "IX_Set_Key_Value").IsUnique();

            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Value).HasMaxLength(256);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("state")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.JobId, "FK_HangFire_State_Job");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Reason).HasMaxLength(100);

            entity.HasOne(d => d.Job).WithMany(p => p.States)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_State_Job");
        });

        modelBuilder.Entity<UserBalance>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("user_balance");

            entity.HasIndex(e => e.UserSeqNum, "USER_SEQ_NUM");

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.Balance)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("BALANCE");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("Created_DATE");
            entity.Property(e => e.PackageSeqNum).HasColumnName("Package_SEQ_NUM");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");

            entity.HasOne(d => d.UserSeqNumNavigation).WithMany(p => p.UserBalances)
                .HasForeignKey(d => d.UserSeqNum)
                .HasConstraintName("user_balance_ibfk_1");
        });

        modelBuilder.Entity<UserComment>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("user_comment");

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.Comment)
                .HasMaxLength(1000)
                .HasColumnName("COMMENT");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_DATE");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.SeqNum).HasName("PRIMARY");

            entity.ToTable("user_info");

            entity.HasIndex(e => e.ContactNumber, "CONTACT_NUMBER").IsUnique();

            entity.HasIndex(e => e.Email, "EMAIL").IsUnique();

            entity.HasIndex(e => e.Roles, "fk_roles");

            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .HasColumnName("ADDRESS_1");
            entity.Property(e => e.Address2)
                .HasMaxLength(255)
                .HasColumnName("ADDRESS_2");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("CITY");
            entity.Property(e => e.ContactNumber).HasColumnName("CONTACT_NUMBER");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .HasColumnName("COUNTRY");
            entity.Property(e => e.DeviceType)
                .HasMaxLength(255)
                .HasColumnName("DEVICE_TYPE");
            entity.Property(e => e.Email).HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsLogin).HasColumnName("IS_LOGIN");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Roles)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ROLES");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("STATE");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("TOKEN");
            entity.Property(e => e.TrailPeriod).HasColumnName("TRAIL_PERIOD");
            entity.Property(e => e.UserBrowser)
                .HasMaxLength(255)
                .HasColumnName("USER_BROWSER");
            entity.Property(e => e.UserHostName)
                .HasMaxLength(255)
                .HasColumnName("USER_HOST_NAME");
            entity.Property(e => e.UserIp)
                .HasMaxLength(255)
                .HasColumnName("USER_IP");
            entity.Property(e => e.Varified).HasColumnName("VARIFIED");
            entity.Property(e => e.ZipPostalCode)
                .HasMaxLength(255)
                .HasColumnName("ZIP_POSTAL_CODE");

            entity.HasOne(d => d.RolesNavigation).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.Roles)
                .HasConstraintName("fk_roles");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("user_roles");

            entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("ROLE_NAME");
            entity.Property(e => e.RoleRights)
                .HasMaxLength(255)
                .HasColumnName("ROLE_RIGHTS");
        });

        modelBuilder.Entity<Userbalanceandrefund>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("userbalanceandrefund");

            entity.Property(e => e.CurBalance)
                .HasPrecision(34, 2)
                .HasColumnName("CUR_BALANCE");
            entity.Property(e => e.Payments)
                .HasPrecision(32, 2)
                .HasColumnName("PAYMENTS");
            entity.Property(e => e.Refund)
                .HasPrecision(32, 2)
                .HasColumnName("REFUND");
            entity.Property(e => e.UserName)
                .HasColumnType("text")
                .HasColumnName("USER_NAME");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");
            entity.Property(e => e.UserTotalImei).HasColumnName("USER_TOTAL_IMEI");
            entity.Property(e => e.UsersExpenditure)
                .HasPrecision(32, 2)
                .HasColumnName("USERS_EXPENDITURE");
        });

        modelBuilder.Entity<ViewUserbalance>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_userbalance");

            entity.Property(e => e.Balance)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .HasColumnName("PACKAGE_NAME");
            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.UserName)
                .HasMaxLength(511)
                .HasDefaultValueSql("''")
                .HasColumnName("USER_Name");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");
        });

        modelBuilder.Entity<VwImeiUserCost>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_imei_user_cost");

            entity.Property(e => e.InfoSeqNum).HasColumnName("INFO_SEQ_NUM");
            entity.Property(e => e.PackageSeqNum).HasColumnName("PACKAGE_SEQ_NUM");
            entity.Property(e => e.PsCreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("PS_CREATED_DATE");
            entity.Property(e => e.SearchDate)
                .HasColumnType("datetime")
                .HasColumnName("SEARCH_DATE");
            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.ServiceFee)
                .HasPrecision(10, 2)
                .HasColumnName("SERVICE_FEE");
            entity.Property(e => e.ServiceNo).HasColumnName("SERVICE_NO");
            entity.Property(e => e.ServiceSeqNum).HasColumnName("SERVICE_SEQ_NUM");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");
        });

        modelBuilder.Entity<VwOrderProgress>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_order_progress");

            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");
        });

        modelBuilder.Entity<VwPaymentdetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_paymentdetail");

            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("AMOUNT");
            entity.Property(e => e.BuyerEmail)
                .HasMaxLength(255)
                .HasColumnName("BUYER_EMAIL");
            entity.Property(e => e.BuyerId)
                .HasMaxLength(255)
                .HasColumnName("BUYER_ID");
            entity.Property(e => e.CreateDatetime)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATETIME");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .HasColumnName("PACKAGE_NAME");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("PAYMENT_METHOD");
            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.Status)
                .HasMaxLength(1000)
                .HasColumnName("STATUS");
            entity.Property(e => e.Tax)
                .HasMaxLength(255)
                .HasColumnName("TAX");
            entity.Property(e => e.TransId)
                .HasMaxLength(255)
                .HasColumnName("TRANS_ID");
            entity.Property(e => e.UpdateDatetime)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATETIME");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");
        });

        modelBuilder.Entity<VwPsJuncionNew4mat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ps_juncion_new4mat");

            entity.Property(e => e.Bronze).HasPrecision(10, 2);
            entity.Property(e => e.Gold).HasPrecision(10, 2);
            entity.Property(e => e.ServiceName)
                .HasMaxLength(100)
                .HasColumnName("SERVICE_NAME");
            entity.Property(e => e.Silver).HasPrecision(10, 2);
        });

        modelBuilder.Entity<VwPsJunction>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ps_junction");

            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasColumnName("COST");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .HasColumnName("PACKAGE_NAME");
            entity.Property(e => e.PackageSeqNum).HasColumnName("PACKAGE_SEQ_NUM");
            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(100)
                .HasColumnName("SERVICE_NAME");
            entity.Property(e => e.ServiceSeqNum).HasColumnName("SERVICE_SEQ_NUM");
        });

        modelBuilder.Entity<VwRefund>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_refund");

            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.BalanceSeqNum).HasColumnName("BALANCE_SEQ_NUM");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
            entity.Property(e => e.PackageSeqNum).HasColumnName("Package_SEQ_NUM");
            entity.Property(e => e.Reason).HasColumnType("text");
            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.ServiceNo)
                .HasMaxLength(100)
                .HasColumnName("Service_no");
            entity.Property(e => e.UserName)
                .HasColumnType("text")
                .HasColumnName("USER_NAME");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");
        });

        modelBuilder.Entity<VwUserImei>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_user_imei");

            entity.Property(e => e.AppleActivationStatusImeiSn)
                .HasColumnType("text")
                .HasColumnName("APPLE ACTIVATION STATUS - IMEI/SN");
            entity.Property(e => e.AppleBasicInfo)
                .HasColumnType("text")
                .HasColumnName("APPLE BASIC INFO");
            entity.Property(e => e.AppleMdmStatus)
                .HasColumnType("text")
                .HasColumnName("APPLE MDM STATUS");
            entity.Property(e => e.BrandModelInfo)
                .HasColumnType("text")
                .HasColumnName("BRAND & MODEL INFO");
            entity.Property(e => e.ClaroAllCountriesPremiumIPhone14)
                .HasColumnType("text")
                .HasColumnName("CLARO ALL COUNTRIES - PREMIUM (iPHONE 14)");
            entity.Property(e => e.ClaroAllCountriesPremiumIPhone15)
                .HasColumnType("text")
                .HasColumnName("CLARO ALL COUNTRIES - PREMIUM (iPHONE 15)");
            entity.Property(e => e.ClaroAllCountriesPremiumUpToIPhone13)
                .HasColumnType("text")
                .HasColumnName("CLARO ALL COUNTRIES - PREMIUM (UP TO iPHONE 13)");
            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("ENTRY_DATE");
            entity.Property(e => e.FullName)
                .HasColumnType("text")
                .HasColumnName("FULL_NAME");
            entity.Property(e => e.GooglePixelInfo)
                .HasColumnType("text")
                .HasColumnName("GOOGLE PIXEL INFO");
            entity.Property(e => e.ICloudOnOff)
                .HasColumnType("text")
                .HasColumnName("iCLOUD ON/OFF");
            entity.Property(e => e.IPhoneCarrierFmiBlacklist)
                .HasColumnType("text")
                .HasColumnName("iPHONE CARRIER - FMI & BLACKLIST");
            entity.Property(e => e.IPhoneSimLock)
                .HasColumnType("text")
                .HasColumnName("iPHONE SIM-LOCK");
            entity.Property(e => e.IdSeq).HasColumnName("ID_SEQ");
            entity.Property(e => e.Imei)
                .HasMaxLength(20)
                .HasColumnName("IMEI");
            entity.Property(e => e.InfoDSeqNum).HasColumnName("INFO_D_SEQ_NUM");
            entity.Property(e => e.InfoSeqNum).HasColumnName("INFO_SEQ_NUM");
            entity.Property(e => e.IphoneCarrier)
                .HasColumnType("text")
                .HasColumnName("IPhone Carrier");
            entity.Property(e => e.MdmLockBypassIPhoneIPad)
                .HasColumnType("text")
                .HasColumnName("MDM LOCK BYPASS - iPHONE/iPAD");
            entity.Property(e => e.MotorolaInfo)
                .HasColumnType("text")
                .HasColumnName("MOTOROLA INFO");
            entity.Property(e => e.OneplusInfo)
                .HasColumnType("text")
                .HasColumnName("ONEPLUS INFO");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .HasColumnName("PACKAGE_NAME");
            entity.Property(e => e.SamsungAtTCricketAllModels)
                .HasColumnType("text")
                .HasColumnName("SAMSUNG AT&T/CRICKET... ALL MODELS");
            entity.Property(e => e.SamsungInfo)
                .HasColumnType("text")
                .HasColumnName("SAMSUNG INFO");
            entity.Property(e => e.SamsungInfoPro)
                .HasColumnType("text")
                .HasColumnName("SAMSUNG INFO - PRO");
            entity.Property(e => e.SearchDate)
                .HasColumnType("datetime")
                .HasColumnName("SEARCH_DATE");
            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.ServiceFee)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("SERVICE_FEE");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(255)
                .HasColumnName("SERVICE_NAME");
            entity.Property(e => e.ServiceNo).HasColumnName("SERVICE_NO");
            entity.Property(e => e.ServiceSeqNum)
                .HasDefaultValueSql("'0'")
                .HasColumnName("SERVICE_SEQ_NUM");
            entity.Property(e => e.TotalCost)
                .HasPrecision(32, 2)
                .HasColumnName("TOTAL_COST");
            entity.Property(e => e.UsaAtTCleanActiveLine)
                .HasColumnType("text")
                .HasColumnName("USA AT&T - CLEAN/ACTIVE LINE");
            entity.Property(e => e.UsaCricketClean6MonthsOld)
                .HasColumnType("text")
                .HasColumnName("USA CRICKET - CLEAN & 6 MONTHS OLD");
            entity.Property(e => e.UsaTMobileClean)
                .HasColumnType("text")
                .HasColumnName("USA T-MOBILE - CLEAN");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");
            entity.Property(e => e.WwBlacklistStatus)
                .HasColumnType("text")
                .HasColumnName("WW BLACKLIST STATUS");
        });

        modelBuilder.Entity<VwUserInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_user_info");

            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .HasColumnName("ADDRESS_1");
            entity.Property(e => e.Address2)
                .HasMaxLength(255)
                .HasColumnName("ADDRESS_2");
            entity.Property(e => e.Balance)
                .HasPrecision(32, 2)
                .HasColumnName("BALANCE");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("CITY");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .HasColumnName("CONTACT_NUMBER");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .HasColumnName("COUNTRY");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsLogin).HasColumnName("IS_LOGIN");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Rights).HasMaxLength(255);
            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("ROLE_NAME");
            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("STATE");
            entity.Property(e => e.TotalBalance)
                .HasPrecision(32, 2)
                .HasColumnName("TOTAL_BALANCE");
            entity.Property(e => e.TrialPeriod).HasColumnName("TRIAL_PERIOD");
            entity.Property(e => e.UserIp)
                .HasMaxLength(255)
                .HasColumnName("USER_IP");
            entity.Property(e => e.UserName)
                .HasColumnType("text")
                .HasColumnName("USER_NAME");
            entity.Property(e => e.UserTotalImei)
                .HasDefaultValueSql("'0'")
                .HasColumnName("USER_TOTAL_IMEI");
            entity.Property(e => e.UsersExpenditure)
                .HasPrecision(32, 2)
                .HasColumnName("USERS_EXPENDITURE");
            entity.Property(e => e.Varified).HasColumnName("VARIFIED");
        });

        modelBuilder.Entity<VwUserPackage>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_user_packages");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");
        });

        modelBuilder.Entity<VwUserPackageCost>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_user_package_cost");

            entity.Property(e => e.Balance)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("BALANCE");
            entity.Property(e => e.BalanceCreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("BALANCE_CREATE_DATE");
            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("COST");
            entity.Property(e => e.PackageSeqNum).HasColumnName("PACKAGE_SEQ_NUM");
            entity.Property(e => e.PsjSeqNum)
                .HasDefaultValueSql("'0'")
                .HasColumnName("PSJ_SEQ_NUM");
            entity.Property(e => e.SeqNum).HasColumnName("SEQ_NUM");
            entity.Property(e => e.ServiceSeqNum).HasColumnName("SERVICE_SEQ_NUM");
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");
        });

        modelBuilder.Entity<VwUserServiceCost>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_user_service_cost");

            entity.Property(e => e.Balance)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("BALANCE");
            entity.Property(e => e.BalanceCreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("BALANCE_CREATED_DATE");
            entity.Property(e => e.CompleteServiceName)
                .HasMaxLength(220)
                .HasColumnName("COMPLETE_SERVICE_NAME");
            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("COST");
            entity.Property(e => e.CostCreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("COST_CREATED_DATE");
            entity.Property(e => e.PackageSeqNum)
                .HasDefaultValueSql("'0'")
                .HasColumnName("Package_SEQ_NUM");
            entity.Property(e => e.Packages).HasMaxLength(100);
            entity.Property(e => e.PsjSeqNum)
                .HasDefaultValueSql("'0'")
                .HasColumnName("PSJ_SEQ_NUM");
            entity.Property(e => e.SeqNum)
                .HasDefaultValueSql("'0'")
                .HasColumnName("SEQ_NUM");
            entity.Property(e => e.ServiceSeqNum)
                .HasDefaultValueSql("'0'")
                .HasColumnName("Service_SEQ_NUM");
            entity.Property(e => e.Services).HasMaxLength(100);
            entity.Property(e => e.UserSeqNum).HasColumnName("USER_SEQ_NUM");
        });

        modelBuilder.Entity<VwUserbalanceNew4mat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_userbalance_new4mat");

            entity.Property(e => e.Bronze).HasPrecision(10, 2);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Gold).HasPrecision(10, 2);
            entity.Property(e => e.Silver).HasPrecision(10, 2);
            entity.Property(e => e.Total)
                .HasPrecision(12, 2)
                .HasColumnName("TOTAL");
            entity.Property(e => e.UserName)
                .HasColumnType("text")
                .HasColumnName("USER_NAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
