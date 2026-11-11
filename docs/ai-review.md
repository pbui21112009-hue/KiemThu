# Thẩm định AI

## Prompt và đầu ra

Prompt tác vụ thực tế: “Viết framework NUnit + Selenium, POM, cấu hình và dữ liệu test. Chạy test, sửa lỗi, tạo log, báo cáo và ảnh khi lỗi… Chi tiết nhiệm vụ ở Notion”. Nguồn bổ sung: ASM1.pdf, Y1–Y10.

Đề xuất AI cho đăng nhập SauceDemo (đầu ra trong phiên triển khai này): hợp lệ; performance_glitch_user; locked_out_user; sai mật khẩu; username trống; password trống; cả hai trống; user không tồn tại; CAPTCHA; quên mật khẩu qua email; đăng nhập bằng Google; chuỗi khoảng trắng.

| Đề xuất | Quyết định | Lý do |
|---|---|---|
| Hợp lệ | GIỮ | Luồng chính, vào Products |
| performance_glitch_user | GIỮ | Chuyển trang chậm, xác minh explicit wait |
| locked_out_user | GIỮ | Tài khoản khóa |
| Sai mật khẩu | GIỮ | Xác thực thất bại |
| Username trống | GIỮ | Biên rỗng |
| Password trống | GIỮ | Biên rỗng |
| Cả hai trống | GIỮ | Thứ tự validation |
| User không tồn tại | GIỮ | Ca lỗi |
| CAPTCHA | BỎ | SauceDemo không có tính năng CAPTCHA |
| Quên mật khẩu qua email | BỎ | Không có luồng email để kiểm tra |
| Đăng nhập Google | BỎ | Không có OAuth trên trang này |
| Khoảng trắng | SỬA | Chưa có đặc tả trim; dùng ca rỗng với kết quả rõ ràng |

## Lỗi AI thực tế

AI sinh CartPage với `By.CssSelector("[data-test='inventory-item-name']")`. Chạy thật lần đầu: 9 pass, 1 fail, `StaleElementReferenceException` tại CartPage.Product. Locator cũng khớp Inventory nên WebDriverWait kết thúc trước khi chuyển sang giỏ; `.Text` đọc phần tử đã bị thay thế. Đã sửa thành `[data-test='cart-list'] [data-test='inventory-item-name']`, chỉ khớp trang đích. Đây là lỗi thực tế, không phải lỗi cố ý dùng để chứng minh CI.

Minh chứng gốc: `screenshots/initial-failure.png`, `docs/evidence/initial.trx`. Bản đầu có thể đối chiếu trong file TRX (stack trace) và đoạn locator ở trên.

## Checklist 6 điểm đối với mã AI

- [x] Locator id/data-test nằm private trong Page; test không chứa locator.
- [x] Chờ tường minh cho input, lỗi và trang đích; không dùng dừng cố định.
- [x] Assert đúng nghiệp vụ: Products, lỗi chính xác, tên và số lượng giỏ.
- [x] Driver riêng từng test, đóng trong teardown, hai fixture chạy song song.
- [x] Dữ liệu demo công khai; không log mật khẩu/token, cấu hình được kiểm tra.
- [x] Đã build và chạy thật; ghi lỗi AI cùng cách sửa. Kết quả chạy lại ghi trong báo cáo, không suy diễn từ checklist.

Đây là thẩm định bằng công cụ do AI thực hiện. Sinh viên vẫn cần đọc, đối chiếu DOM và ký xác nhận trước khi nộp; không ghi nhận thay cho duyệt của con người.
