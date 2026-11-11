# Báo cáo ASM - Kiểm thử nâng cao cùng AI

Nguồn: ASM1.pdf do người dùng cung cấp; trang Notion ASS kiểm thử. Ngày thực hiện: 11/11/2026. Repository public: https://github.com/phuongvhn2009/KiemThu. PR: https://github.com/phuongvhn2009/KiemThu/pull/1.

## Y1 - Môi trường

.NET SDK 10.0.403, NUnit 4.3.2, adapter 5.0.0, Selenium 4.52.0. Build thành công. NuGet phát cảnh báo NU1900 do không lấy được dữ liệu audit qua SSL; không tắt audit để che cảnh báo. Cốc Cốc 154.0.8037.106 dùng ChromeDriver 154.0.8037.92. Chrome 157 được dùng trong các lần kiểm tra ban đầu; CI cấu hình Chrome headless theo đề.

## Y2 - Luồng tự động

Login hợp lệ xác nhận Products. Luồng thứ hai thêm Sauce Labs Backpack, mở giỏ và kiểm tra tên cùng quantity=1. Locator id/data-test; test không dùng locator trực tiếp.

## Y3 - Chờ tường minh

BasePage dùng WebDriverWait, kiểm tra hiển thị/khả dụng, xử lý phần tử chưa có và DOM thay thế trong lúc tìm. Ca performance_glitch_user chuyển trang chậm. Không có lệnh dừng cố định trong mã nguồn. Kết quả lặp lại và thời gian lấy từ TRX ở phần đo thực tế.

## Y4 - POM

BasePage, LoginPage, InventoryPage, CartPage; locator private. Ví dụ đổi id login-button: sửa field submit trong LoginPage một chỗ; các test và JSON giữ nguyên. Locator giỏ được giới hạn trong cart-list để chờ đúng trang.

## Y5 - Cấu hình và dữ liệu

appsettings.json tách URL/browser/timeout/headless. Settings kiểm tra HTTPS, timeout dương và browser được hỗ trợ. TEST_BROWSER dùng cho CI; CHROME_BINARY và CHROMEDRIVER_DIRECTORY phục vụ executable/driver Cốc Cốc cài khác vị trí. BaseTest tạo và đóng driver từng test. JSON có 8 ca TestCaseSource: hợp lệ, chậm, khóa, sai mật khẩu, user rỗng, password rỗng, cả hai rỗng, user không tồn tại.

## Y6 - Runner, log, báo cáo

ParallelScope.Fixtures, hai worker, LoginTests và CartTests chạy độc lập. Mỗi test có thư mục artifact riêng, log START/END và kết quả; thất bại chụp PNG, đính kèm NUnit. Teardown vẫn đóng driver nếu chụp ảnh lỗi. TRX xuất qua dotnet test; CI upload cả TestResults bằng always(). TEST_ARTIFACTS xác định nơi lưu log/ảnh. Các phép đo là một mẫu, chịu ảnh hưởng mạng và tải máy, không coi là benchmark kết luận chung.

## Y7 - Thẩm định AI

Chi tiết prompt, đầu ra, bảng GIỮ/BỎ/SỬA, 3 ca bị loại và checklist 6 điểm ở ai-review.md. Lỗi AI thật: locator tên sản phẩm của CartPage khớp Inventory khi đang chuyển trang; lần đầu 9 pass/1 fail, stale element tại CartPage.Product. Sửa selector bằng cart-list, chạy lại giỏ đạt. Ảnh ban đầu có thể đã hiển thị giỏ vì trình duyệt hoàn tất chuyển trang trước lúc teardown chụp; stack trace TRX là bằng chứng lỗi thời điểm đọc DOM. Thẩm định công cụ chưa thay thế chữ ký duyệt của sinh viên.

## Y8 - Git và PR

Nhánh feat/saucedemo-framework, Conventional Commits, PR #1 thật. Plugin GitHub trả 403 khi tạo PR; đã dùng GitHub API với credential Git sẵn có, không in/commit credential. Mô phỏng conflict trong repo biệt lập .artifacts/conflict: main đổi Timeout=15, feature đổi Timeout=20 từ Timeout=10; merge phát CONFLICT, đọc hai phía, chọn 20 theo yêu cầu feature rồi add/commit. Lịch sử và marker gốc ở evidence/conflict.txt; repo sản phẩm không bị thay đổi bởi mô phỏng.

Bản clone sạch nhánh PR đã restore/build không cảnh báo và chạy Cốc Cốc đạt 10/10 (evidence/clean.trx). Merge theo yêu cầu người dùng với giới hạn CI billing được công khai; không đánh đồng local xanh với GitHub Actions xanh.

## Y9 - CI và điểm chặn

Workflow tests.yml chạy push, PR vào main và workflow_dispatch, Ubuntu, .NET 10, Chrome headless, TRX và artifact luôn được upload nếu runner khởi động. GitHub từ chối khởi động runner vì billing account bị khóa. Thông báo thật: “The job was not started because your account is locked due to a billing issue.” Xem evidence/ci-billing.json và run https://github.com/phuongvhn2009/KiemThu/actions/runs/43004820116.

Commit 0c89245 cố ý assert sai title để minh chứng CI; đã khôi phục ở 8ea657b. Vì runner không khởi động, không được tính đây là CI đỏ do assertion; chưa có CI xanh. Chủ tài khoản cần xử lý billing rồi rerun workflow; sau đó tái thực hiện đỏ do assertion trên nhánh thử và sửa xanh. Không đổi billing hay thanh toán thay người dùng.

## Y10 - Bàn giao

README có badge, stack, cấu trúc, hướng dẫn Cốc Cốc và minh họa lỗi thật. Báo cáo PDF kèm thẩm định AI. Repo public. Profile (pin, bio, avatar) chưa thay đổi vì cần lựa chọn cá nhân; chưa có ảnh CI xanh/profile. Thư mục screenshots chỉ chứa ảnh thật; không tạo ảnh giả cho các mục còn thiếu. Dữ liệu đăng nhập là tài khoản demo công khai, không phải bí mật cá nhân.

## Việc còn lại để nộp đủ điểm

Gỡ khóa billing và có CI xanh, chứng minh CI đỏ do test rồi sửa xanh, hoàn thiện hồ sơ GitHub và các ảnh còn thiếu. Sinh viên tự đọc checklist AI, đối chiếu yêu cầu và xác nhận trước khi nộp. Trạng thái trong Notion phải phân biệt phần đã chạy được và phần bị chặn.
