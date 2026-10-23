-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 19, 2026 at 03:25 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_pengkar`
--

-- --------------------------------------------------------

--
-- Table structure for table `detail_penggajian`
--

CREATE TABLE `detail_penggajian` (
  `id_detail` int(11) NOT NULL,
  `id_penggajian` int(11) NOT NULL,
  `nama_komponen` int(100) NOT NULL,
  `jenis` varchar(20) NOT NULL,
  `jumlah` decimal(15,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tabsensi`
--

CREATE TABLE `tabsensi` (
  `id_absensi` int(11) NOT NULL,
  `id_karyawan` int(11) NOT NULL,
  `tanggal` date DEFAULT NULL,
  `jam_masuk` time NOT NULL,
  `jam_keluar` time NOT NULL,
  `status` varchar(20) NOT NULL,
  `keterangan` varchar(255) NOT NULL,
  `lembur` decimal(5,2) DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tabsensi`
--

INSERT INTO `tabsensi` (`id_absensi`, `id_karyawan`, `tanggal`, `jam_masuk`, `jam_keluar`, `status`, `keterangan`, `lembur`) VALUES
(6, 0, '2026-09-01', '07:30:00', '17:00:00', 'Hadir', '--', 0.00),
(7, 0, '2026-09-01', '07:30:00', '18:00:00', 'Hadir', '--', 1.00),
(8, 0, '2026-09-01', '07:45:00', '19:00:00', 'Hadir', 'Terlambat', 2.00),
(9, 0, '2026-09-01', '07:30:00', '16:30:00', 'Hadir', '--', 0.00),
(10, 0, '2026-09-01', '07:30:00', '20:30:00', 'Hadir', 'Lembur', 3.00),
(11, 0, '2026-09-01', '00:00:00', '00:00:00', 'Izin', 'Keperluan keluarga', 0.00);

-- --------------------------------------------------------

--
-- Table structure for table `tjabatan`
--

CREATE TABLE `tjabatan` (
  `id_jabatan` int(11) NOT NULL,
  `nama_jabatan` varchar(100) NOT NULL,
  `gaji_pokok` int(11) NOT NULL,
  `tunjangan_jabatan` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tjabatan`
--

INSERT INTO `tjabatan` (`id_jabatan`, `nama_jabatan`, `gaji_pokok`, `tunjangan_jabatan`) VALUES
(5, 'Staff', 4000000, 500000),
(6, 'Supervisor', 6000000, 1000000),
(7, 'Manager', 9000000, 2000000);

-- --------------------------------------------------------

--
-- Table structure for table `tkaryawan`
--

CREATE TABLE `tkaryawan` (
  `id_karyawan` int(11) NOT NULL,
  `nik` varchar(20) NOT NULL,
  `nama_karyawan` varchar(100) NOT NULL,
  `jenis_kelamin` varchar(20) NOT NULL,
  `alamat` text NOT NULL,
  `nomor_telp` varchar(20) NOT NULL,
  `tanggal_masuk` date DEFAULT NULL,
  `id_jabatan` int(11) DEFAULT NULL,
  `status` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tkaryawan`
--

INSERT INTO `tkaryawan` (`id_karyawan`, `nik`, `nama_karyawan`, `jenis_kelamin`, `alamat`, `nomor_telp`, `tanggal_masuk`, `id_jabatan`, `status`) VALUES
(2, '0001222092007', 'laura', 'perempuan', 'Kp.Cikuya Lebak', '0882001989292', '2023-09-01', 5, 'aktif'),
(3, '0001213092007', 'fachra', 'Perempuan', 'paster', '098654345', '2026-09-09', 7, 'Aktif');

-- --------------------------------------------------------

--
-- Table structure for table `tpenggajian`
--

CREATE TABLE `tpenggajian` (
  `id_penggajian` int(11) NOT NULL,
  `id_karyawan` int(11) NOT NULL,
  `periode_bulan` int(11) NOT NULL,
  `periode_tahun` int(11) NOT NULL,
  `gaji_pokok` decimal(15,2) NOT NULL,
  `tunjangan` decimal(15,2) NOT NULL,
  `lembur` decimal(15,2) NOT NULL,
  `potongan` decimal(15,2) NOT NULL,
  `gaji_bersih` decimal(15,2) NOT NULL,
  `tanggal_proses` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `trole`
--

CREATE TABLE `trole` (
  `id_role` int(11) NOT NULL,
  `role_name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `trole`
--

INSERT INTO `trole` (`id_role`, `role_name`) VALUES
(14, 'HR'),
(15, 'HRD');

-- --------------------------------------------------------

--
-- Table structure for table `tusers`
--

CREATE TABLE `tusers` (
  `id_user` int(11) NOT NULL,
  `username` varchar(100) NOT NULL,
  `password` varchar(250) NOT NULL,
  `nama_karyawan` varchar(100) NOT NULL,
  `id_role` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tusers`
--

INSERT INTO `tusers` (`id_user`, `username`, `password`, `nama_karyawan`, `id_role`) VALUES
(2, 'laura', 'c4ca4238a0b923820dcc509a6f75849b', 'laura', 14),
(6, 'fachra', '2', 'fachra', 15);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `detail_penggajian`
--
ALTER TABLE `detail_penggajian`
  ADD PRIMARY KEY (`id_detail`);

--
-- Indexes for table `tabsensi`
--
ALTER TABLE `tabsensi`
  ADD PRIMARY KEY (`id_absensi`);

--
-- Indexes for table `tjabatan`
--
ALTER TABLE `tjabatan`
  ADD PRIMARY KEY (`id_jabatan`);

--
-- Indexes for table `tkaryawan`
--
ALTER TABLE `tkaryawan`
  ADD PRIMARY KEY (`id_karyawan`);

--
-- Indexes for table `tpenggajian`
--
ALTER TABLE `tpenggajian`
  ADD PRIMARY KEY (`id_penggajian`);

--
-- Indexes for table `trole`
--
ALTER TABLE `trole`
  ADD PRIMARY KEY (`id_role`);

--
-- Indexes for table `tusers`
--
ALTER TABLE `tusers`
  ADD PRIMARY KEY (`id_user`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `detail_penggajian`
--
ALTER TABLE `detail_penggajian`
  MODIFY `id_detail` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tabsensi`
--
ALTER TABLE `tabsensi`
  MODIFY `id_absensi` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `tjabatan`
--
ALTER TABLE `tjabatan`
  MODIFY `id_jabatan` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `tkaryawan`
--
ALTER TABLE `tkaryawan`
  MODIFY `id_karyawan` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `tpenggajian`
--
ALTER TABLE `tpenggajian`
  MODIFY `id_penggajian` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `trole`
--
ALTER TABLE `trole`
  MODIFY `id_role` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `tusers`
--
ALTER TABLE `tusers`
  MODIFY `id_user` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
