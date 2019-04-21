﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperChat.Data;

namespace SuperChat.Data.Migrations
{
    [DbContext(typeof(SuperChatContext))]
    [Migration("20190421152543_updates")]
    partial class updates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SuperChat.Domain.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("SuperChat.Domain.Key", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChatId");

                    b.Property<byte[]>("KeyBytes");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Keys");
                });

            modelBuilder.Entity("SuperChat.Domain.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChatId");

                    b.Property<string>("Content");

                    b.Property<byte[]>("IV");

                    b.Property<int?>("RecieverId");

                    b.Property<int?>("SenderId");

                    b.Property<DateTime>("TimeSend");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("RecieverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SuperChat.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("PublicKey");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SuperChat.Domain.Key", b =>
                {
                    b.HasOne("SuperChat.Domain.Chat", "Chat")
                        .WithMany("Keys")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SuperChat.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SuperChat.Domain.Message", b =>
                {
                    b.HasOne("SuperChat.Domain.Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId");

                    b.HasOne("SuperChat.Domain.User", "Reciever")
                        .WithMany()
                        .HasForeignKey("RecieverId");

                    b.HasOne("SuperChat.Domain.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");
                });
#pragma warning restore 612, 618
        }
    }
}
