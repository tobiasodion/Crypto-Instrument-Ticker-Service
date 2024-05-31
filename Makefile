PROJECT_DIR = TickerSubscription
PROJECT_FILE = $(PROJECT_DIR)/TickerSubscription.csproj

BUILD_CMD = dotnet build $(PROJECT_FILE)
RUN_CMD = dotnet run --project $(PROJECT_FILE)

all: build run

build:
	@echo "Building the project..."
	$(BUILD_CMD)

run: build
	@echo "Running the project..."
	$(RUN_CMD)

clean:
	@echo "Cleaning the project..."
	dotnet clean $(PROJECT_FILE)

restore:
	@echo "Restoring dependencies..."
	dotnet restore $(PROJECT_FILE)

.PHONY: all build run clean restore
