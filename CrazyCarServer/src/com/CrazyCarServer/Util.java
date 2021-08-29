package com.CrazyCarServer;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

public class Util {
	public static class JDBC{
		// MySQL 8.0 ���ϰ汾 - JDBC �����������ݿ� URL
		static final String JDBC_DRIVER = "com.mysql.cj.jdbc.Driver";
		static final String DB_URL = "jdbc:mysql://localhost:3306/crazy_car?"
				+ "useSSL=false&allowPublicKeyRetrieval=true&serverTimezone=UTC";

		// ���ݿ���û��������룬��Ҫ�����Լ�������
		static final String USER = "root";
		static final String PASS = "164728";

		public static String ExecuteSelect(String sql) {
			System.out.println("ExecuteSelect sql = " + sql);
			Connection conn = null;
			Statement stmt = null;
			try {
				// ע�� JDBC ����
				Class.forName(JDBC_DRIVER);

				// ������
				//System.out.println("�������ݿ�...");
				conn = DriverManager.getConnection(DB_URL, USER, PASS);

				// ִ�в�ѯ
				//System.out.println(" ʵ����Statement����...");
				stmt = conn.createStatement();
				ResultSet rs = stmt.executeQuery(sql);
				String resultStr = null;
				// չ����������ݿ�
				while (rs.next()) {
					// ͨ���ֶμ���
					resultStr = rs.getString("user_password");
					System.out.println("ExecuteSelect : user_password = " + resultStr);
				}
				// ��ɺ�ر�
				System.out.println("ExecuteSelect  Finish");
				rs.close();
				stmt.close();
				conn.close();
				return resultStr;
			} catch (SQLException se) {
				// ���� JDBC ����
				se.printStackTrace();
			} catch (Exception e) {
				// ���� Class.forName ����
				e.printStackTrace();
				return null;
			} finally {
				// �ر���Դ
				try {
					if (stmt != null)
						stmt.close();
				} catch (SQLException se2) {
				} // ʲô������
				try {
					if (conn != null)
						conn.close();
				} catch (SQLException se) {
					se.printStackTrace();
				}
			}

			System.out.println("Goodbye!");
			return null;
		}
		
		public static void ExecuteInsert(String sql) {
			Connection conn = null;
			System.out.println("ExecuteInsert sql = " + sql);	
			try {
				// ע�� JDBC ����
				Class.forName(JDBC_DRIVER);
				conn = DriverManager.getConnection(DB_URL, USER, PASS);

				PreparedStatement pst = conn.prepareStatement(sql);
	            pst.executeUpdate();
				System.out.println(" ExecuteInsert Finish " + pst);
				conn.close();
				return;
			} catch (SQLException se) {
				// ���� JDBC ����
				se.printStackTrace();
			} catch (Exception e) {
				// ���� Class.forName ����
				e.printStackTrace();
				return;
			} finally {
				// �ر���Դ
				try {
					if (conn != null)
						conn.close();
				} catch (SQLException se) {
					se.printStackTrace();
				}
			}

			System.out.println("Goodbye!");
			return;
		}
	}
}