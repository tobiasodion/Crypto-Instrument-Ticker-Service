PROJECT_DIR = TickerSubscription
PROJECT_FILE = $(PROJECT_DIR)/TickerSubscription.csproj

BUILD_CMD = dotnet build $(PROJECT_FILE)
RUN_CMD = dotnet run --project $(PROJECT_FILE)

TEST_PROJECT_DIR = TickerSubscription.Tests
TEST_PROJECT_FILE = $(TEST_PROJECT_DIR)/TickerSubscription.Tests.csproj

TEST_BUILD_CMD = dotnet build $(TEST_PROJECT_FILE)
TEST_RUN_CMD = dotnet test $(TEST_PROJECT_FILE)

all: build test run

build:
	@echo "Building the project..."
	$(BUILD_CMD)

build-test:
	@echo "Building the test project..."
	$(TEST_BUILD_CMD)

test: build-test
	@echo "Testing the project..."
	$(TEST_RUN_CMD)

run: build
	@echo "Running the project..."
	$(RUN_CMD)

clean:
	@echo "Cleaning the project..."
	dotnet clean $(PROJECT_FILE)

restore:
	@echo "Restoring dependencies..."
	dotnet restore $(PROJECT_FILE)

.PHONY: all build test run clean restore
