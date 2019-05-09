﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperChat.Data;

namespace SuperChat.Data.Migrations
{
    [DbContext(typeof(SuperChatContext))]
    partial class SuperChatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SuperChat.Domain.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("SuperChat.Domain.Key", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChatId");

                    b.Property<byte[]>("KeyBytes");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Keys");
                });

            modelBuilder.Entity("SuperChat.Domain.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ChatId");

                    b.Property<string>("Content");

                    b.Property<byte[]>("Iv");

                    b.Property<int>("RecieverId");

                    b.Property<int>("SenderId");

                    b.Property<DateTime>("TimeSend");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SuperChat.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("PublicKey");

                    b.Property<string>("Salt");

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
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SuperChat.Domain.Message", b =>
                {
                    b.HasOne("SuperChat.Domain.Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId");
                });
#pragma warning restore 612, 618
        }
    }
}
